using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelManager", menuName = "Managers/LevelManager")]
public class LevelManager : BaseInjectable, IAwake, IStart
{
    private PopupManager _popupManager;

    private FortressController _fortressController;
    private EnemyController _enemyController;
    private LevelManagerMonoBehaviour _monoBehaviour;

    private Fortress _fortress;
    private List<Enemy> _enemies;

    private float _enemyTimerDuration = 5.0f;
    private float _enemyTimerDurationModifier = 0.5f;
    private float _previousTime = 0.0f;

    public bool IsGameOver { get; set; }
    public Fortress Fortress => _fortress;

    public void OnAwake()
    {
        _popupManager = InjectBox.Get<PopupManager>();
        _fortressController = new FortressController();
        _enemyController = new EnemyController();
        _monoBehaviour = GameObject.Find("[EnterPoint]").GetComponent<LevelManagerMonoBehaviour>();
        _monoBehaviour.SetUp(this);
        _enemies = new List<Enemy>();
    }

    public void OnStart()
    {
        _popupManager.ShowPopup(nameof(LevelPopup));
        _fortressController = new FortressController();

        _fortress = _fortressController.SpawnFortress();
        _enemies.Add(_enemyController.SpawnEnemy());

        EventManager.Subscribe<OnEnemyIsDeadEvent>(OnEnemyIsDead);
        EventManager.Subscribe<OnGameOverEvent>(OnGameOver);
    }
    public override void OnUpdate()
    {
       if(!IsGameOver) SpawnEnemyByTimer();
    }

    private void SpawnEnemyByTimer()
    {
        if (Time.time - _previousTime > _enemyTimerDuration)
        {
            _enemies.Add(_enemyController.SpawnEnemy());
            _previousTime = Time.time;
        }
    }

    public List<Enemy> GetEnemiesList() => _enemies;

    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }

    private void OnEnemyIsDead(OnEnemyIsDeadEvent obj)
    {
        if (_enemyTimerDuration > 1.0f)
            _enemyTimerDuration -= _enemyTimerDurationModifier;
    }
    
    private void OnGameOver(OnGameOverEvent obj)
    {
        IsGameOver = true;
    }

}