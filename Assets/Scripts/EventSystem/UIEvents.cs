using System;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    public static UIEvents Current;

    private void Awake()
    {
        Current = this;
    }

    public Action OnButtonStartGame;
    public void ButtonStartGame()
    {
        OnButtonStartGame?.Invoke();
    }

    public Action OnButtonShop;
    public void ButtonShop()
    {
        OnButtonShop?.Invoke();
    }

    public Action OnButtonMainMenu;
    public void ButtonMainMenu()
    {
        OnButtonMainMenu?.Invoke();
    }

    public Action OnButtonPauseGame;
    public void ButtonPauseGame()
    {
        OnButtonPauseGame?.Invoke();
    }

    public Action OnButtonResumeGame;
    public void ButtonResumeGame()
    {
        OnButtonResumeGame?.Invoke();
    }

    public Action OnButtonNextLevel;
    public void ButtonNextLevel()
    {
        OnButtonNextLevel?.Invoke();
    }

    public Action OnButtonRestartGame;
    public void ButtonRestartGame()
    {
        OnButtonRestartGame?.Invoke();
    }

    public Action OnButtonExitGame;
    public void ButtonExitGame()
    {
        OnButtonExitGame?.Invoke();
    }

    public Action<PlayerSkinUIView> OnButtonBuySkin;
    public void ButtonBuySkin(PlayerSkinUIView skin)
    {
        OnButtonBuySkin?.Invoke(skin);
    }

    public Action<PlayerSkinUIView> OnButtonSelectSkin;
    public void ButtonSelectSkin(PlayerSkinUIView skin)
    {
        OnButtonSelectSkin?.Invoke(skin);
    }

    public Action<PlayerSkinUIView> OnButtonGetSkinByReward;
    public void ButtonGetSkinByReward(PlayerSkinUIView skin)
    {
        OnButtonGetSkinByReward?.Invoke(skin);
    }
}