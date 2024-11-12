using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFinishGame : UICanvas
{
    [SerializeField] private Button resetGameButton;
    [SerializeField] private Button retryButton;

    void OnEnable() {
        resetGameButton.onClick.AddListener(ResetGame);
        retryButton.onClick.AddListener(RetryLevel);
    }

    private void OnDisable() {
        resetGameButton.onClick.RemoveListener(ResetGame);
        retryButton.onClick.RemoveListener(RetryLevel);
    }

    private void ResetGame() {
        GameManager.Instance.OnResetGame();
    }

    private void RetryLevel() {
        GameManager.Instance.OnRetryLevel();
    }

}
