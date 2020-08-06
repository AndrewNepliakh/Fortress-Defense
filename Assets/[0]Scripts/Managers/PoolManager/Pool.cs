using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private Dictionary<int, Queue<GameObject>> _objects = new Dictionary<int, Queue<GameObject>>();
    private Dictionary<int, int> _IDs = new Dictionary<int, int>();
    private Transform _parentPool;

    public void InitPool(Transform parent)
    {
        _parentPool = parent;
    }

    public GameObject Activate(GameObject prefab, Transform parent, Vector3 position = default,
        Vector3 rotation = default)
    {
        var key = prefab.GetInstanceID();
        var hasPool = _objects.TryGetValue(key, out var pool);

        if (hasPool && pool.Count > 0)
        {
            var transform = pool.Dequeue().transform;
            transform.SetParent(parent);
            transform.gameObject.SetActive(true);
            transform.rotation = Quaternion.Euler(rotation);
            transform.localPosition = position;
            transform.GetComponent<IPoolable>()?.OnActivate();
            return transform.gameObject;
        }

        if (!hasPool) _objects.Add(key, new Queue<GameObject>());

        GameObject go;
        go = Instantiate(prefab, position, Quaternion.Euler(rotation), parent);
        if (parent) go.transform.localPosition = position;
        go.name = prefab.name + go.GetInstanceID();
        go.GetComponent<IPoolable>()?.OnActivate();
        _IDs.Add(go.GetInstanceID(), key);
        return go;
    }

    public void Deactivate(GameObject go)
    {
        _objects[_IDs[go.GetInstanceID()]].Enqueue(go);
        go.GetComponent<IPoolable>()?.OnDeactivate();
        go.transform.SetParent(_parentPool);
        go.SetActive(false);
    }

    public void ClearPool()
    {
        _parentPool = null;
        _objects.Clear();
        _IDs.Clear();
    }
}