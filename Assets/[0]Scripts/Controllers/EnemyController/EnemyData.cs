using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EnemyID
{
    BatSword = 0,
    Bomber = 10,
    Onager = 20
}

[Serializable]
public class EnemyModel
{
    public EnemyID ID;
    public GameObject Prefab;
    public Vector3 SpawnPosition;
}

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
public class EnemyData : BaseInjectable
{
    public List<EnemyModel> EnemyModels;

    public EnemyModel GetEnemyModelByID(EnemyID id)
    {
        return EnemyModels.Find(enemyModel => enemyModel.ID == id);
    }

    public List<EnemyModel> GetAllEnemyModels() => EnemyModels;

}