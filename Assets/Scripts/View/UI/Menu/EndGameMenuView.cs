using UnityEngine;
using UnityEngine.UI;

public class EndGameMenuView : BaseMenuView
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;

    [Header("Elements")]
    [SerializeField] private Text _headerText;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _restartButton;

    // Will be needed later
    //[SerializeField] private TextMesh _moneyByLevelText;
    //[SerializeField] private Button _scaleForAdButton;

    private UIController _controller;


    private void Start()
    {
        FindMyController();
        _nextLevelButton.onClick.AddListener(UIEvents.Current.ButtonNextLevel);
        _restartButton.onClick.AddListener(UIEvents.Current.ButtonRestartGame);
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
    public void ActivateState(bool inLevelComplete)
    {
        switch (inLevelComplete)
        {
            case true:
                _nextLevelButton.gameObject.SetActive(true);
                _restartButton.gameObject.SetActive(false);
                _headerText.text = "Complete!";
                break;
            case false:
                _nextLevelButton.gameObject.SetActive(false);
                _restartButton.gameObject.SetActive(true);
                _headerText.text = "Lose :(";
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
}