using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class to inherit all popups (screens)
/// </summary>
public abstract class BasePopup : MonoBehaviour, IPoolable
{
    protected PoolManager PoolManager;
    protected PopupManager PopupManager;
    
    /// <summary>
    /// Method to show popup(screen), at same time it is Initialise popup method  
    /// </summary>
    /// <param name="obj"></param>
    public void Show(object obj = null)
    {
        PoolManager = InjectBox.Get<PoolManager>();
        PopupManager = InjectBox.Get<PopupManager>();

        OnShow(obj);
    }

    /// <summary>
    /// Method to close popup(screen)
    /// </summary>
    public void Close(object obj = null)
    {
        OnClose(obj);
    }
    
    protected virtual void OnShow(object obj = null){}
    protected virtual void OnClose(object obj = null){}

    public virtual void OnActivate(object argument = default)
    {
        gameObject.SetActive(true);
    }

    public virtual void OnDeactivate(object argument = default)
    {
        gameObject.SetActive(false);
    }

}
