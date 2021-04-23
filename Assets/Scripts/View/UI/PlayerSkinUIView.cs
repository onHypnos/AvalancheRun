using UnityEngine;
using UnityEngine.UI;

public class PlayerSkinUIView : MonoBehaviour
{
    [SerializeField] private Sprite _screenShot;
    [SerializeField] private SkinRarity _rarity;
    [SerializeField] private SkinType _type;
    [SerializeField] private SkinState _state;
    [SerializeField] private int _price;

    private SaveDataRepo _saveData;

    public Sprite ScreenShot => _screenShot;
    public SkinRarity Rarity => _rarity;
    public SkinType Type => _type;
    public SkinState State => _state;
    public int Price => _price;


    public void ChangeState(SkinState state)
    {
        _state = state;
    }
}