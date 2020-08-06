using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManagerMonoBehaviour : MonoBehaviour
{
  
    private BaseInjectable _manager;
    
    public void SetUp<T>(T manager) where T : BaseInjectable
    {
        _manager = manager;
    }

    private void Update()
    {
        _manager?.OnUpdate();
    }

    public void DoCoroutine(IEnumerator coroutine)
    {
       
    }
}
