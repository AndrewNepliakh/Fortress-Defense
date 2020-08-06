using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneEnterPoint : Singleton<SceneEnterPoint>
{
    [SerializeField] private List<BaseInjectable> Injectables = new List<BaseInjectable>();
    
    private void Awake()
    {
        foreach (var inject in Injectables)
        {
            InjectBox.Add(inject);
        }
        
        InjectBox.InitializeStartInjectables();
        
    }
    
    private void OnApplicationQuit()
    {
        InjectBox.DisableAll();
    }
}