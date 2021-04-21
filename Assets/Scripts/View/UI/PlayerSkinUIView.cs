using UnityEngine;
using UnityEngine.UI;

public class PlayerSkinUIView : MonoBehaviour
{
    [SerializeField] private Image _screenShot;
    [SerializeField] private SkinRarity _rarity;
    [SerializeField] private SkinType _type;
    [SerializeField] private SkinState _state;
    [SerializeField] private int _price;

    private SaveDataRepo _saveData;

    public Image ScreenShot => _screenShot;
    public SkinRarity Rarity => _rarity;
    public SkinType Type => _type;
    public SkinState State => _state;
    public int Price => _price;


    private void Awake()
    {
        _saveData = new SaveDataRepo();
        int stateNumber = _saveData.LoadInt(gameObject.name);

        switch (stateNumber)
        {
            case 0:
                ChangeState(SkinState.Locked);
                    break;
            case 1:
                ChangeState(SkinState.Unlocked);
                    break;
            case 2:
                ChangeState(SkinState.Selected);
                break;
        }
    }

    public void ChangeState(SkinState state)
    {
        _state = state;
    }
}