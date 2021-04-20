using UnityEngine;

public class SkinView : BaseObjectView
{
    public GameObject[] Skins;

    private SkinController _controller;


    private void Start()
    {
        FindMyController();
    }

    private void FindMyController()
    {
        if (_controller == null)
        {
            _controller = FindObjectOfType<MainController>().GetController<SkinController>();
        }
        _controller.AddView(this);
    }

    private void OnDestroy()
    {
        _controller.RemoveView(this);
    }
}