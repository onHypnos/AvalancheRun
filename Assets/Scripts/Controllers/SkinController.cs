using System.Collections.Generic;
using UnityEngine;

public class SkinController : BaseController
{
    private List<SkinView> _skinViews = new List<SkinView>();


    public SkinController(MainController main) : base(main)
    {

    }

    public override void Initialize()
    {
        base.Initialize();

        GameEvents.Current.OnSelectSkin += SelectSkin;
    }

    public void AddView(SkinView view)
    {
        if (!_skinViews.Contains(view))
        {
            _skinViews.Add(view);
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log($"Object {view} was already added to update list");
#endif
        }

        switch (view.gameObject.tag)
        {
            case TagManager.Member:
                view.SelectRandomSkin();
                break;
            case TagManager.Player:
                //TODO
                break;
            default:
                break;
        }
    }

    public void RemoveView(SkinView view)
    {
        if (_skinViews.Contains(view))
        {
            _skinViews.Remove(view);
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log($"Object {view} was already removed from update list");
#endif
        }
    }

    private void SelectSkin(string skinName)
    {
        if (_skinViews.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < _skinViews.Count; i++)
        {
            if (_skinViews[i].gameObject.tag == TagManager.Player)
            {
                _skinViews[i].SelectSkinByName(skinName);
            }
        }
    }
}