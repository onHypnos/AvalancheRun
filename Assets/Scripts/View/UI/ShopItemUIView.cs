using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUIView : MonoBehaviour
{
    [SerializeField] private Button _button;

    [Header("Button sprites")]
    [SerializeField] private Sprite _spriteLocked;
    [SerializeField] private Sprite _spriteBuy;
    [SerializeField] private Sprite _spriteGetForReward;
    [SerializeField] private Sprite _spriteSelect;
    [SerializeField] private Sprite _spriteSelected;

    private SaveDataRepo _saveData;


    public void SetButton(PlayerSkinUIView skin)
    {
        switch (skin.State)
        {
            case SkinState.Locked:
                switch (skin.Type)
                {
                    case SkinType.DailyReward:
                        _button.GetComponent<Image>().sprite = _spriteLocked;
                        _button.GetComponentInChildren<TextMeshProUGUI>().text = "LOCKED";
                        _button.interactable = false;
                        break;
                    case SkinType.KostyaDollar:
                        _button.GetComponent<Image>().sprite = _spriteBuy;
                        _button.GetComponentInChildren<TextMeshProUGUI>().text = $"{skin.Price}";

                        //TODO AddListener to buying skin

                        _saveData = new SaveDataRepo();
                        if (_saveData.LoadInt(SaveKeyManager.Bank) >= skin.Price)
                        {
                            _button.interactable = true;
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

                        //TODO AddListener to reward video

                        break;
                }
                break;

            case SkinState.Unlocked:
                _button.GetComponent<Image>().sprite = _spriteSelect;
                _button.GetComponentInChildren<TextMeshProUGUI>().text = "SELECT";
                _button.interactable = true;

                //TODO AddListener to select skin

                break;

            case SkinState.Selected:
                _button.GetComponent<Image>().sprite = _spriteSelected;
                _button.GetComponentInChildren<TextMeshProUGUI>().text = "SELECTED";
                _button.interactable = false;
                break;
        }
    }
}