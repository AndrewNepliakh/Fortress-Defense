using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that represent menu screen
/// </summary>
public class MenuPopup : BasePopup
{
    public void OnClick_PlayGameButton()
    {
        StageManager.LoadStage(StageID.Level);
    }

    public void OnClick_MoreGameButton()
    {
    }

    public void OnClick_QuitGameButton()
    {
        Application.Quit();
    }
}