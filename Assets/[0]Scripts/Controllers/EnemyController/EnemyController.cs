using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController
{
    private PoolManager _poolManager;
    private EnemyData _enemyData;
    private List<EnemyModel> _enemyModels;
    private Vector3 _spawnPoint;

    public EnemyController()
    {
        _poolManager = InjectBox.Get<PoolManager>();
        _enemyData = InjectBox.Get<EnemyData>();
        _enemyModels = _enemyData.GetAllEnemyModels();
    }


    public Enemy SpawnEnemy()
    {
        var enemyModel = GetRandomEnemyPrefab();
        return _poolManager.GetOrCreate<Enemy>(enemyModel.Prefab, null, enemyModel.SpawnPosition);
    }

    private EnemyModel GetRandomEnemyPrefab()
    {
        var random = Random.Range(0, _enemyModels.Count);
        return _enemyModels[random];
    }
}
