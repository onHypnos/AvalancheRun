using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    [SerializeField] private Transform _playerStarterPoint;
    [SerializeField] private PlayerView _playerPrefab;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private CameraView _mainCameraPrefab;
    [SerializeField] private bool _useMouse = true;
    [SerializeField] private string _starterSceneName = "";

    private List<BaseController> _controllers = new List<BaseController>();
    private InputController _input;
    private PlayerController _playerController;   
    private CameraController _cameraMain;
    private EnemyController _enemyController;
    private GameModeController _gameMode;
    private CollectableController _collectables;
    private UIController _uiController;

    public bool UseMouse => _useMouse;
    public InputController Input => _input;
    public GameModeController GameMode => _gameMode;
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //SceneManager.UnloadSceneAsync(_starterSceneName);

        _gameMode = new GameModeController(this);
        _input = new InputController(this);        
        _playerController = new PlayerController(this);
        if (_playerStarterPoint == null)
        {
            _playerStarterPoint = FindObjectOfType<PlayerStarterPosition>().transform;
        }
        _playerView = Instantiate(_playerPrefab, _playerStarterPoint.position, Quaternion.identity);
        //_playerView = _playerPrefab;
        _playerController.SetPlayer(_playerView);
        _cameraMain = new CameraController(this);
        _cameraMain.SetCamera(_mainCameraPrefab);
        _cameraMain.SetPursuedObject(_playerView.gameObject);
        _enemyController = new EnemyController(this);
        _collectables = new CollectableController(this);

        _uiController = new UIController(this);
    }

    private void Start()
    {
        foreach (BaseController controller in _controllers)
        {
            if (controller is IInitialize)
            {
                controller.Initialize();
            }            
        }

        _gameMode.LoadNewScene(_starterSceneName);
    }

    

    private void Update()
    {
        foreach (BaseController controller in _controllers)
        {
            if (controller is IExecute)
            {
                controller.Execute();
            }            
        }        
    }

    private void LateUpdate()
    {        
        foreach (BaseController controller in _controllers)
        {
            if (controller is ILateExecute)
            {
                controller.LateExecute();
            }
        }        
    }


    /// <summary>
    /// Add controller in Controller's list
    /// </summary>
    /// <param name="controller"></param>
    public void AddController(BaseController controller)
    {
        if (!_controllers.Contains(controller))
        {
            _controllers.Add(controller);
        }
    }

    /// <summary>
    /// Remove controller from Controller's list
    /// </summary>
    /// <param name="controller"></param>
    public void RemoveController(BaseController controller)
    {
        if (_controllers.Contains(controller))
        {
            _controllers.Remove(controller);
        }
    }
 
    /// <summary>
    /// Return controller's instance from controller's list
    /// </summary>
    /// <param Type="type"></param>
    /// <returns></returns>
    public T GetController<T>() where T : BaseController
    {
        foreach (BaseController obj in _controllers)
        {
            if (obj.GetType() == typeof(T))
            {
                return (T)obj;
            }
        }
        return null;
    }

    #region Will Replaced or Deleted

    /// <summary>
    /// don't forget syntaxis
    /// </summary>
    public void stupid()
    {        
        GetController<BaseController>();
        InputController a = new InputController(this);
        a = GetController<InputController>();
    }
    #endregion
}
