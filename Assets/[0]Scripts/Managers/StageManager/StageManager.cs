using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Class to handle scene transitions
/// </summary>
public class StageManager 
{
    /// <summary>
    /// Method the same as SceneManager.LoadScene() but with ability to adjust InjectBox, clear resources and call garbage collector. 
    /// </summary>
    /// <param name="stageId"></param>
    /// <param name="mode"></param>
    public static void LoadStage(StageID stageId, LoadSceneMode mode = LoadSceneMode.Single)
    {
        InjectBox.ClearNonGlobalInjectables();
        InjectBox.Get<PoolManager>().ClearPools();
        EventManager.UnsubscribeAllEvents();
        SceneManager.LoadScene(stageId.ToString(), mode);
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }
}
