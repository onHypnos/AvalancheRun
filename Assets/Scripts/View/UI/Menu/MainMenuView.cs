using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : BaseMenuView
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;

    [Header("Elements")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _shopButton;

    private UIController _controller;


    private void Awake()
    {
        FindMyController();
        _startButton.onClick.AddListener(UIEvents.Current.ButtonStartGame);
        //TODO обновление текста количества монет
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
}