using System.Collections;
using System.Collections.Generic;
using Game;
using MyGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletePanel : ShowHidable
{
    [SerializeField]private List<string> _toasts = new List<string>();

    protected override void OnShowCompleted()
    {
        base.OnShowCompleted();
    }


    public void OnClickContinue()
    {
        UIManager.Instance.LoadNextLevel();
    }
}