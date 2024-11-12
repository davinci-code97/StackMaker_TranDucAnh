using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum GameState { MainMenu, GamePlay, ClearLevel, FinishGame, Setting }

public class GameManager : Singleton<GameManager>
{
    private static GameState gameState;

    public int currentLevel;
    public bool isRetry;

    private void Awake() {
        //tranh viec nguoi choi cham da diem vao man hinh
        Input.multiTouchEnabled = false;
        //target frame rate ve 60 fps
        Application.targetFrameRate = 60;
        //tranh viec tat man hinh
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //xu tai tho
        int maxScreenHeight = 1920;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight) {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
    }

    void Start()
    {
        currentLevel = UserDataManager.Instance.GetLevel();
        ChangeState(GameState.MainMenu);
    }

    public bool IsState(GameState state) => gameState == state;

    public void ChangeState(GameState state) {
        gameState = state;
        switch (state) {
            case GameState.MainMenu:
                OnMainMenu();
                break;
            case GameState.GamePlay:
                OnPlayingGame();
                break;
            case GameState.ClearLevel:
                OnClearLevel();
                break;
            case GameState.FinishGame:
                OnFinishGame();
                break;
            case GameState.Setting:
                OnSettingMenu();
                break;
            default:
                break;
        }
    }

    private void OnMainMenu() {
        OnSetUpLevel();
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<UIMainMenu>();
    }

    private void OnPlayingGame() {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<UIGamePlaying>();
    }

    public void OnClearLevel() {
        currentLevel++;
        if (LevelManager.Instance.IsFinishGame) {
            currentLevel--;
            ChangeState(GameState.FinishGame);
        } else {
            currentLevel--;
            UIManager.Instance.CloseAll();
            UIManager.Instance.OpenUI<UIWin>();
        }
    }

    public void OnNextLevel() { 
        currentLevel++;
        UserDataManager.Instance.SetLevel(currentLevel);
        OnSetUpLevel();
        ChangeState(GameState.GamePlay);
    }


    private void OnFinishGame() {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<UIFinishGame>();
    }

    private void OnSettingMenu() {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<UISetting>();
    }

    private void OnSetUpLevel() {
        LevelManager.Instance.DestroyCurrentMap();
        LevelManager.Instance.LoadMap(currentLevel);
        SetUpPlayer();
    }

    public void OnRetryLevel() {
        OnSetUpLevel();
        ChangeState(GameState.GamePlay);
    }

    public void OnResetGame() {
        UserDataManager.Instance.SetLevel(0);
        currentLevel = 0;
        ChangeState(GameState.MainMenu);
    }

    private void SetUpPlayer() {
        Player.Instance.ClearAllBricks();
        Player.Instance.transform.position = LevelManager.Instance.GetCurrentStartPoint();
    }

}
