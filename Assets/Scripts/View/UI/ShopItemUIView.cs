using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUIView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _skinImage;

    [Header("Advertise icon")]
    [SerializeField] private Image _advertiseIcon;

    [Header("Button sprites")]
    [SerializeField] private Sprite _spriteLocked;
    [SerializeField] private Sprite _spriteBuy;
    [SerializeField] private Sprite _spriteGetForReward;
    [SerializeField] private Sprite _spriteSelect;
    [SerializeField] private Sprite _spriteSelected;

    [Header("Background sprites")]
    [SerializeField] private Sprite _commonBG;
    [SerializeField] private Sprite _rareBG;
    [SerializeField] private Sprite _legendaryBG;

    private SaveDataRepo _saveData;


    public void SetupItem(PlayerSkinUIView skin)
    {
        //Button
        switch (skin.State)
        {
            case SkinState.Selected:
                _button.GetComponent<Image>().sprite = _spriteSelected;
                _button.GetComponentInChildren<TextMeshProUGUI>().text = "SELECTED";
                _button.interactable = false;
                _advertiseIcon.gameObject.SetActive(false);
                break;
            case SkinState.Unlocked:
                _button.GetComponent<Image>().sprite = _spriteSelect;
                _button.GetComponentInChildren<TextMeshProUGUI>().text = "SELECT";
                _button.interactable = true;
                _button.onClick.AddListener(() => UIEvents.Current.ButtonSelectSkin(skin));
                _advertiseIcon.gameObject.SetActive(false);
                break;
            case SkinState.Locked:
                switch (skin.Type)
                {
                    case SkinType.DailyReward:
                        _button.GetComponent<Image>().sprite = _spriteLocked;
                        _button.GetComponentInChildren<TextMeshProUGUI>().text = "LOCKED";
                        _button.interactable = false;
                        _advertiseIcon.gameObject.SetActive(false);
                        break;
                    case SkinType.KostyaDollar:
                        _button.GetComponent<Image>().sprite = _spriteBuy;
                        _button.GetComponentInChildren<TextMeshProUGUI>().text = $"{skin.Price}";
                        _advertiseIcon.gameObject.SetActive(false);

                        _saveData = new SaveDataRepo();
                        if (_saveData.LoadInt(SaveKeyManager.Bank) >= skin.Price)
                        {
                            _button.interactable = true;
                            _button.onClick.AddListener(() => UIEvents.Current.ButtonBuySkin(skin));
                        }
                        else
                        {
                            _button.interactable = false;
                        }
                        break;
                    case SkinType.ShopReward:
                        _button.GetComponent<Image>().sprite = _spriteGetForReward;
                        _button.GetComponentInChildren<TextMeshProUGUI>().text = "GET";
                        _button.interactable = true;
                        _button.onClick.AddListener(() => UIEvents.Current.ButtonGetSkinByReward(skin));
                        _advertiseIcon.gameObject.SetActive(true);
                        break;
                }
                break;
        }

        //Background
        switch (skin.Rarity)
        {
            case SkinRarity.Common:
                GetComponent<Image>().sprite = _commonBG;
                break;
            case SkinRarity.Rare:
                GetComponent<Image>().sprite = _rareBG;
                break;
            case SkinRarity.Legendary:
                GetComponent<Image>().sprite = _legendaryBG;
                break;
        }

        //Skin image
        _skinImage.sprite = skin.ScreenShot;
    }
}