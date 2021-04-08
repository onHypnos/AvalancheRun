using UnityEngine;
using UnityEngine.UI;

public class EndGameMenuView : BaseMenuView
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;

    [Header("Elements")]
    [SerializeField] private TextMesh _headerText;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _restartButton;

    private UIController _controller;


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
        //TODO
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