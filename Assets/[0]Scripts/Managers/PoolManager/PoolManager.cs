using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolManager", menuName = "Managers/PoolManager")]
public class PoolManager : BaseInjectable, IGlobal
{
    private static Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();
    private GameObject _poolsGO;

    public Pool AddPool(string keyName)
    {
        _poolsGO = GameObject.Find("[POOLS]") ?? new GameObject("[POOLS]");

        if (!_pools.ContainsKey(keyName))
        {
            var poolGO = new GameObject("Pool: " + keyName);
            poolGO.transform.SetParent(_poolsGO.transform);
            var pool = poolGO.AddComponent<Pool>();
            pool.InitPool(poolGO.transform);
            _pools.Add(keyName, pool);
            return pool;
        }

        return GetPool(keyName);
    }

    public Pool GetPool(string keyName)
    {
        var formatedKeyName = keyName.Split('-')[0];
        if (_pools.TryGetValue(formatedKeyName, out var pool)) return pool;
        return null;
    }

    public GameObject GetOrCreate(GameObject prefab, Transform parent, Vector3 position = default, Vector3 rotation = default)
    {
        return AddPool(prefab.name).Activate(prefab, parent, position, rotation);
    }

    public T GetOrCreate<T>(GameObject prefab, Transform parent = null, Vector3 position = default, Vector3 rotation = default) where T : MonoBehaviour, IPoolable
    {
        return AddPool(prefab.name).Activate(prefab, parent, position, rotation).GetComponent<T>();
    }

    public void Release(string keyName, GameObject go)
    {
        _pools[keyName.Split('-')[0]].Deactivate(go);
    }

    public void PreLoad(GameObject prefab, int count)
    {
        Transform parent;

        try
        {
            parent = GameObject.Find(prefab.name + "s").transform;
        }
        catch (NullReferenceException e)
        {
            parent = new GameObject(prefab.name + "s").transform;
        }


        var poolables = new List<GameObject>();

        for (int i = 0; i < count; i++)
        {
            poolables.Add(GetOrCreate(prefab, parent));
        }

        foreach (var poolable in poolables)
        {
            GetPool(prefab.name).Deactivate(poolable);
        }
    }

    public void ClearPools()
    {
        _poolsGO = null;
        _pools.Clear();
    }
}