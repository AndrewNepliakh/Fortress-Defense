using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UIManager", menuName = "Managers/UIManager")]
public class PopupManager : BaseInjectable, IAwake
{
    private Transform _uiParent;
    private PoolManager _poolManager;

    private Dictionary<string, BasePopup> _allPopups = new Dictionary<string, BasePopup>();

    public void OnAwake()
    {
        try
        {
            _uiParent = GameObject.Find("UI_Canvas").GetComponent<Transform>();
        }
        catch (NullReferenceException e)
        {
            var eventSystemGo = new GameObject("EventSystem");
            eventSystemGo.AddComponent<EventSystem>();
            eventSystemGo.AddComponent<StandaloneInputModule>();

            var canvasGo = new GameObject("UI_Canvas");
            var canvas = canvasGo.AddComponent<Canvas>();
            var scaler = canvasGo.AddComponent<CanvasScaler>();
            canvasGo.AddComponent<GraphicRaycaster>();

            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920.0f, 1080.0f);

            _uiParent = canvasGo.transform;
        }

        _poolManager = InjectBox.Get<PoolManager>();
    }

    public BasePopup ShowPopup(string type, object obj = null)
    {
        var popupPrefab = Resources.Load<GameObject>("Prefabs/UI/Popups/" + type);
        //var popupPrefabPosition = popupPrefab.GetComponent<RectTransform>().position;
        var popup = _poolManager.GetOrCreate<BasePopup>(popupPrefab, _uiParent);
        _allPopups.Add(type, popup);
        popup.Show(obj);
        return popup;
    }

    public void ClosePopup(string type, object obj = null)
    {
        if (_allPopups.TryGetValue(type, out var popup))
        {
            _poolManager.GetPool(type)?.Deactivate(popup.gameObject);
            popup.Close(obj);
        }
    }

    public BasePopup GetPopup(string type)
    {
        if (_allPopups.TryGetValue(type, out var popup))  return popup;
        return null;
    }
}