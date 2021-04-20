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
    }

    public void AddView (SkinView view)
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
                for (int i = 0; i < view.Skins.Length; i++)
                {
                    view.Skins[i].SetActive(false);
                }
                view.Skins[Random.Range(0, view.Skins.Length)].SetActive(true);
                break;
            case TagManager.Player:
                //TODO
                break;
            default:
                break;
        }
    }

    public void RemoveView (SkinView view)
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
}