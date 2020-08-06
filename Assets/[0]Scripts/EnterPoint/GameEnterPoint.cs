using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// The Class the application is starts from
/// </summary>
public class GameEnterPoint : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private UserManager _userManager;
    [SerializeField] private PoolManager _poolManager;
    private void Awake()
    {
        InjectBox.Add(_gameManager);
        InjectBox.Add(_userManager);
        InjectBox.Add(_poolManager);
    }

    private void Start()
    {
        _gameManager.Initialize();
        StageManager.LoadStage(StageID.Menu);
    }
}
