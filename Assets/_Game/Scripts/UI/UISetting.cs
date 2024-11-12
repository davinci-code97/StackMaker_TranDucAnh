using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : UICanvas
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainmenuButton;

    void OnEnable()
    {
        closeButton.onClick.AddListener(ResumeGame);
        retryButton.onClick.AddListener(RetryLevel);
        mainmenuButton.onClick.AddListener(BackToMainMenu);
    }

    private void ResumeGame() {
        GameManager.Instance.ChangeState(GameState.GamePlay);
    }

    private void BackToMainMenu() {
        GameManager.Instance.ChangeState(GameState.MainMenu);
    }

    private void RetryLevel() {
        GameManager.Instance.OnRetryLevel();
    }



}
