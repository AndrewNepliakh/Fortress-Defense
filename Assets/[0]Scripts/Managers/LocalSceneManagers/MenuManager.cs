
using UnityEngine;

/// <summary>
/// Class to handle all processes of game menu
/// </summary>

[CreateAssetMenu(fileName = "MenuManager", menuName = "Managers/MenuManager")]
public class MenuManager : BaseInjectable, IAwake, IStart
{
    private PopupManager _popupManager;
    
    public void OnAwake()
    {
        _popupManager = InjectBox.Get<PopupManager>();
    }

    public void OnStart()
    {
        _popupManager.ShowPopup(nameof(MenuPopup));
    }
    
}