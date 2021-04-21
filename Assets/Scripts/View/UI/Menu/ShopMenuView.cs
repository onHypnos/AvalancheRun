using UnityEngine;
using UnityEngine.UI;

public class ShopMenuView : BaseMenuView
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;

    [Header("Shop items")]
    [SerializeField] private ShopItemUIView[] _shopItems;

    [Header("Button close")]
    [SerializeField] private Button _buttonClose;

    private PlayerSkinUIView[] _skins;
    private UIController _controller;


    private void Awake()
    {
        FindMyController();

        _buttonClose.onClick.AddListener(UIEvents.Current.ButtonMainMenu);

        _skins = _controller.Player.gameObject.GetComponentsInChildren<PlayerSkinUIView>(true);
        SortSkins();

        SetupItems();

        //TODO отображение бабла
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
                for (int j = i + 1; j < _skins.Length; j++)
                {
                    if (_skins[j].State == SkinState.Selected)
                    {
                        _skins[j].ChangeState(SkinState.Unlocked);
                    }
                }

                Swap(ref _skins[i], ref _skins[0]);
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

        //Cycle 2
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
        for (int i = 0; i < _shopItems.Length; i ++)
        {
            _shopItems[i].SetupItem(_skins[i]);
        }
    }
}