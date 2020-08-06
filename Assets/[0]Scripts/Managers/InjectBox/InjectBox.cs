#define DEBUG // Define constant to debugging

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// Class to contain and maintain injectables.
/// Injectables are Scriptable Objects that represent managers and are stored in a dictionary to avoid singletons.
/// </summary>
public class InjectBox : Singleton<InjectBox>
{
    private Dictionary<Type, object> _injectables = new Dictionary<Type, object>();

    public static void Add(object obj)
    {
        Instance._injectables.Add(obj.GetType(), obj);
    }

    public static void InitializeStartInjectables()
    {
#if (DEBUG)
        foreach (Transform children in Instance.transform)
        {
            Destroy(children.gameObject);
        }
#endif

        var o = Instance.gameObject;
        o.AddComponent<ManagerMonoBehaviour>();

        var injectablesList = Instance._injectables.Values.ToList();

        foreach (var injectable in injectablesList)
        {
            if (injectable is IAwake)
            {
                ((IAwake) injectable).OnAwake();
            }
#if (DEBUG)
            var monitor = new GameObject("[" + injectable + "]");
            monitor.transform.SetParent(Instance.transform);
#endif
        }

        foreach (var injectable in injectablesList)
        {
            if (injectable is IStart)
            {
                ((IStart) injectable).OnStart();
            }
        }

        foreach (var injectable in injectablesList)
        {
            if (injectable is ILateStart)
            {
                ((ILateStart) injectable).OnLateStart();
            }
        }
    }

    public static T Get<T>()
    {
        Instance._injectables.TryGetValue(typeof(T), out var manager);
        return (T) manager;
    }

    public static void DisableAll()
    {
        var injectablesList = Instance._injectables.Values.ToList();

        foreach (var injectable in injectablesList)
        {
            if (injectable is IDisable)
            {
                ((IDisable) injectable).LocalDisable();
            }
        }
    }

    public static void ClearNonGlobalInjectables()
    {
        var injectablesList = Instance._injectables.Values.ToList();
        
        foreach (var injectable in injectablesList)
        {
            if (injectable is IGlobal) continue;
            Instance._injectables.Remove(injectable.GetType());
        }
    }
}