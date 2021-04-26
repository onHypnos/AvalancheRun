using UnityEngine;

public class UIController : BaseController, IExecute
{
    private MainMenuView _mainMenu;
    private InGameView _inGame;
    private PauseMenuView _pauseMenu;
    private EndGameMenuView _endGameMenu;
    private ShopMenuView _shopMenu;
    private TutorialUIView _tutorial;

    private PlayerView _player;
    private GameObject _uiPrefab;
    private SaveDataRepo _saveData;

    public PlayerView Player => _player;


    public UIController(MainController main, PlayerView player) : base(main)
    {
        _player = player;
        _saveData = new SaveDataRepo();
    }

    public override void Initialize()
    {
        base.Initialize();
        /*_uiPrefab = Resources.Load<GameObject>("UIPrefabs/Ui");
        if (_uiPrefab != null)
        {
            GameObject.Instantiate(_uiPrefab,_main.transform);
        }
        else
        {
            Debug.LogError("Cant load UI prefab");
            //Application.Quit();
        }*/
        GameObject.Instantiate(Resources.Load<GameObject>("UIPrefabs/Ui"));
        UIEvents.Current.OnButtonStartGame += StartGame;
        UIEvents.Current.OnButtonPauseGame += PauseGame;
        UIEvents.Current.OnButtonResumeGame += ResumeGame;
        UIEvents.Current.OnButtonNextLevel += NextLevel;
        UIEvents.Current.OnButtonRestartGame += RestartGame;
        UIEvents.Current.OnButtonShop += OpenShop;
        UIEvents.Current.OnButtonMainMenu += OpenMainMenu;
        UIEvents.Current.OnButtonBuySkin += BuySkin;
        UIEvents.Current.OnButtonSelectSkin += SelectSkin;
        UIEvents.Current.OnButtonGetSkinByReward += AskRewardSkin;
        UIEvents.Current.OnButtonAddMoneyByReward += AskRewardMoney;

        GameEvents.Current.OnLevelComplete += WinGame;
        GameEvents.Current.OnLevelFailed += LoseGame;

        if (IsNewDay())
        {
            GameEvents.Current.OnLevelStart += ShowTutorial;
        }

        SwitchUI(UIState.MainMenu);
    }

    public override void Execute()
    {
        base.Execute();
    }

    private void OpenMainMenu()
    {
        SwitchUI(UIState.MainMenu);
    }

    private void OpenShop()
    {
        SwitchUI(UIState.Shop);
    }

    private void StartGame()
    {
        SwitchUI(UIState.InGame);
        GameEvents.Current.PlayerControllerSetActive(true);
        GameEvents.Current.LevelStart();
    }
    private void PauseGame()
    {
        SwitchUI(UIState.Pause);
        GameEvents.Current.GamePaused();
    }
    private void ResumeGame()
    {
        SwitchUI(UIState.InGame);
        GameEvents.Current.GameResumed();
    }
    private void WinGame()
    {
        SwitchUI(UIState.EndGame);
        _endGameMenu.ActivateState(true);
    }
    private void LoseGame()
    {
        SwitchUI(UIState.EndGame);
        _endGameMenu.ActivateState(false);
    }
    private void NextLevel()
    {
        SwitchUI(UIState.MainMenu);
        GameEvents.Current.NextLevel();
    }
    private void RestartGame()
    {
        SwitchUI(UIState.MainMenu);
        GameEvents.Current.LevelRestart();
    }
    private void BuySkin(PlayerSkinUIView skin)
    {
        GameEvents.Current.RemoveMoney(skin.Price);
    }
    private void SelectSkin(PlayerSkinUIView skin)
    {
        GameEvents.Current.SelectSkin(skin.gameObject.name);
    }

    public void AddView(MainMenuView view)
    {
        _mainMenu = view;
    }
    public void AddView(InGameView view)
    {
        _inGame = view;
    }
    public void AddView(PauseMenuView view)
    {
        _pauseMenu = view;
    }
    public void AddView(EndGameMenuView view)
    {
        _endGameMenu = view;
    }
    public void AddView(ShopMenuView view)
    {
        _shopMenu = view;
    }
    public void AddView(TutorialUIView view)
    {
        _tutorial = view;
    }

    public void AskRewardSkin(PlayerSkinUIView skin)
    {
        GameEvents.Current.AskingRewardedVideo(new SkinRewardModel(skin));
    }
    public void AskRewardMoney()
    {
        GameEvents.Current.AskingRewardedVideo(new MoneyRewardModel());
    }

    private void SwitchUI(UIState state)
    {
        switch (state)
        {
            case UIState.MainMenu:
                _mainMenu.Show();
                _inGame.Hide();
                _pauseMenu.Hide();
                _endGameMenu.Hide();
                _shopMenu.Hide();
                break;
            case UIState.InGame:
                _mainMenu.Hide();
                _inGame.Show();
                _pauseMenu.Hide();
                _endGameMenu.Hide();
                _shopMenu.Hide();
                break;
            case UIState.Pause:
                _mainMenu.Hide();
                _inGame.Hide();
                _pauseMenu.Show();
                _endGameMenu.Hide();
                _shopMenu.Hide();
                break;
            case UIState.EndGame:
                _mainMenu.Hide();
                _inGame.Hide();
                _pauseMenu.Hide();
                _endGameMenu.Show();
                _shopMenu.Hide();
                break;
            case UIState.Shop:
                _mainMenu.Hide();
                _inGame.Hide();
                _pauseMenu.Hide();
                _endGameMenu.Hide();
                _shopMenu.Show();
                break;
        }
    }

    private bool IsNewDay()
    {
        if (System.DateTime.Now.Day == _saveData.LoadInt(SaveKeyManager.DayNumber))
        {
            return false;
        }
        else
        {
            _saveData.SaveData(System.DateTime.Now.Day, SaveKeyManager.DayNumber);
            return true;
        }
    }

    private void ShowTutorial()
    {
        if (_tutorial != null)
        {
            _tutorial.Show();
            GameEvents.Current.OnLevelStart -= ShowTutorial;
            InputEvents.current.OnTouchBeganEvent += CloseTutorial;
        }
    }

    private void CloseTutorial(Vector2 huita)
    {
        _tutorial.Hide();
        InputEvents.current.OnTouchBeganEvent -= CloseTutorial;
    }
}