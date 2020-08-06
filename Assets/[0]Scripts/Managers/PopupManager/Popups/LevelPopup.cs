using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPopup : BasePopup
{
    [SerializeField] private GameObject _gameOverPopup;

    protected override void OnShow(object obj = null)
    {
       EventManager.Subscribe<OnGameOverEvent>(OnGameOver);
    }

    private void OnGameOver(OnGameOverEvent obj)
    {
        _gameOverPopup.SetActive(true);
    }
}
