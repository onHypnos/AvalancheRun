using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopMenuView : BaseMenuView
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;

    [Header("Money")]
    [SerializeField] private TextMeshProUGUI _textMoney;

    [Header("Button close")]
    [SerializeField] private Button _buttonClose;

    [Header("Shop items")]
    [SerializeField] private ShopItemUIView[] _shopItems;

    private PlayerSkinUIView[] _skins;
    private UIController _controller;
    private SaveDataRepo _saveData;
    private int _bankUI;


    private void Awake()
    {
        _saveData = new SaveDataRepo();
        _bankUI = _saveData.LoadInt(SaveKeyManager.Bank);

        FindMyController();

        _buttonClose.onClick.AddListener(UIEvents.Current.ButtonMainMenu);

        _skins = _controller.Player.gameObject.GetComponentsInChildren<PlayerSkinUIView>(true);
        SortSkins();

        SetupItems();

        UpdateMoneyText();

        UIEvents.Current.OnButtonBuySkin += BuyingSkin;
        UIEvents.Current.OnButtonSelectSkin += SelectingSkin;
        GameEvents.Current.OnUnlockSkinEvent += UnlockSkin;
    }

    public override void Hide()
    {
        if (!IsShow) return;
        _panel.gameObject.SetActive(false);
        IsShow = false;
    }
    public override void Show()
    {
        if (IsShow) return;
        _panel.gameObject.SetActive(true);
        IsShow = true;
    }

    public void FindMyController()
    {
        if (_controller == null)
        {
            _controller = FindObjectOfType<MainController>().GetController<UIController>();
        }
        _controller.AddView(this);
    }

    private void SortSkins()
    {
        int count = 0;
        //Cycle 1
        for (int i = 0; i < _skins.Length; i++)
        {
            //For state SELECTED
            if (_skins[i].State == SkinState.Selected)
            {
                Swap(ref _skins[i], ref _skins[0]);
            }

        }
        //Cycle 2
        for (int i = count + 1; i < _skins.Length; i++)
        {
            //One more selected insurance
            if (_skins[i].State == SkinState.Selected)
            {
                _skins[i].ChangeState(SkinState.Unlocked);
            }
            //Common skins
            if (_skins[i].Rarity == SkinRarity.Common)
            {
                count++;
                if (i > count)
                {
                    Swap(ref _skins[i], ref _skins[count]);
                }
            }
        }
        //Cycle 3
        for (int i = count + 1; i < _skins.Length; i++)
        {
            //Rare skins
            if (_skins[i].Rarity == SkinRarity.Rare)
            {
                count++;
                if (i > count)
                {
                    Swap(ref _skins[i], ref _skins[count]);
                }
            }
        }
    }

    private void Swap(ref PlayerSkinUIView skin1, ref PlayerSkinUIView skin2)
    {
        PlayerSkinUIView temp = skin1;
        skin1 = skin2;
        skin2 = temp;
    }

    private void SetupItems()
    {
        int stateID;
        SkinState state;

        Debug.LogWarning("Skins debug mode. In case of release please open code below");
        for (int i = 0; i < _shopItems.Length; i++)
        {
            ////Открыть перед релизом:
            //stateID = _saveData.LoadInt(_skins[i].gameObject.name);
            //switch (stateID)
            //{
            //    case 0:
            //        state = SkinState.Locked;
            //        break;
            //    case 1:
            //        state = SkinState.Unlocked;
            //        break;
            //    case 2:
            //        state = SkinState.Selected;
            //        break;
            //    default:
            //        state = SkinState.Locked;
            //        break;
            //}
            //_skins[i].ChangeState(state);

            _shopItems[i].SetupItem(_skins[i]);
        }
    }

    private void UpdateMoneyText()
    {
        _textMoney.text = $"{_bankUI}";
    }

    private void BuyingSkin(PlayerSkinUIView skin)
    {
        _bankUI -= skin.Price;
        UpdateMoneyText();

        skin.ChangeState(SkinState.Unlocked);
        //_saveData.SaveData(1, skin.gameObject.name);

        SetupItems();
    }

    private void SelectingSkin(PlayerSkinUIView skin)
    {
        for (int i = 0; i < _skins.Length; i++)
        {
            if (_skins[i].State == SkinState.Selected)
            {
                _skins[i].ChangeState(SkinState.Unlocked);
                _saveData.SaveData(1, _skins[i].gameObject.name);
            }
        }

        skin.ChangeState(SkinState.Selected);
        //_saveData.SaveData(2, skin.gameObject.name);

        SortSkins();
        SetupItems();
    }

    private void UnlockSkin(PlayerSkinUIView skin)
    {

        skin.ChangeState(SkinState.Unlocked);
        //_saveData.SaveData(1, skin.gameObject.name);

        SetupItems();
    }
}