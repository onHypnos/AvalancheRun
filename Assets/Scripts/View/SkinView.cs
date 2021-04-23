using UnityEngine;

public class SkinView : BaseObjectView
{
    [SerializeField] private GameObject[] _skins;

    private SkinController _controller;

    public GameObject[] Skins => _skins;


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

    public void SelectRandomSkin()
    {
        for (int i = 0; i < _skins.Length; i++)
        {
            _skins[i].SetActive(false);
        }
        _skins[Random.Range(0, _skins.Length)].SetActive(true);
    }

    public void SelectSkinByName(string skinName)
    {
        for (int i = 0; i < _skins.Length; i++)
        {
            _skins[i].SetActive(false);

            if (_skins[i].gameObject.name == skinName)
            {
                _skins[i].SetActive(true);
            }
        }
    }

    private void OnDestroy()
    {
        if(this.isActiveAndEnabled)
        _controller.RemoveView(this);
    }
}