using System;
using Game;
using I2.Loc;
using MyGame;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayPanel : ShowHidable
{
    [SerializeField] private Button _undoBtn;
    [SerializeField] private Text _lvlTxt;

    private void Start()
    {
        LocalizedString locString = "Level";
        _lvlTxt.text = locString + " " + LevelManager.Instance.Level.no;
    }

    public void OnClickUndo()
    {
        LevelManager.Instance.OnClickUndo();
    }

    public void OnClickRestart()
    {
        GameManager.LoadGame(new LoadGameData
        {
            Level = LevelManager.Instance.Level,
            LevelGroup = LevelManager.Instance.LevelGroup,
        },false);
    }

    public void OnClickSkip()
    {
//        if (!AdsManager.IsVideoAvailable())
//        {
//            SharedUIManager.PopUpPanel.ShowAsInfo("Connection?","Sorry no video ads available.Check your internet connection!");
//            return;
//        }

//        SharedUIManager.PopUpPanel.ShowAsConfirmation("Skip","Watch Video to skip this level", success =>
//        {
//            if(!success)
//                return;

//            AdsManager.ShowVideoAds(true, s =>
//            {
//                if(!s)
//                    return;
////                ResourceManager.CompleteLevel(LevelManager.Instance.GameMode, LevelManager.Instance.Level.no);
//                UIManager.Instance.LoadNextLevel();
//            });
          
//        });
    }

    public void OnClickMenu()
    {
        //SharedUIManager.PopUpPanel.ShowAsConfirmation("Exit?","Are you sure want to exit the game?", success =>
        //{
        //    if(!success)
        //        return;

        //    GameManager.LoadScene("MainMenu");
        //});

        GameManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        _undoBtn.interactable = LevelManager.Instance.HasUndo;
    }
}