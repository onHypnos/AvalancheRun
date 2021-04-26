using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameMenuView : BaseMenuView
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;

    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Image _moneyIcon;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private Button _scaleMoneyButton;

    private UIController _controller;


    private void Awake()
    {
        FindMyController();
        _nextLevelButton.onClick.AddListener(UIEvents.Current.ButtonNextLevel);
        _restartButton.onClick.AddListener(UIEvents.Current.ButtonRestartGame);
        _scaleMoneyButton.onClick.AddListener(UIEvents.Current.ButtonAddMoneyByReward);

        GameEvents.Current.OnGetCurrentMoney += SetMoneyText;
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
    public void ActivateState(bool isLevelComplete)
    {
        switch (isLevelComplete)
        {
            case true:
                _nextLevelButton.gameObject.SetActive(true);
                _restartButton.gameObject.SetActive(false);
                _headerText.text = "Complete!";
                _moneyIcon.gameObject.SetActive(true);
                _moneyText.gameObject.SetActive(true);
                _scaleMoneyButton.gameObject.SetActive(true);
                break;
            case false:
                _nextLevelButton.gameObject.SetActive(false);
                _restartButton.gameObject.SetActive(true);
                _headerText.text = "Lose :(";
                _moneyIcon.gameObject.SetActive(false);
                _moneyText.gameObject.SetActive(false);
                _scaleMoneyButton.gameObject.SetActive(false);
                break;
        }
    }

    public void FindMyController()
    {
        if (_controller == null)
        {
            _controller = FindObjectOfType<MainController>().GetController<UIController>();
        }
        _controller.AddView(this);
    }

    private void SetMoneyText(int value)
    {
        _moneyText.text = $"{value}";
    }
}