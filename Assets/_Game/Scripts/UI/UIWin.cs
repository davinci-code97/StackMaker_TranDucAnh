using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWin : UICanvas
{
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainmenuButton;

    void OnEnable() {
        nextLevelButton.onClick.AddListener(NextLevel);
        retryButton.onClick.AddListener(RetryLevel);
        mainmenuButton.onClick.AddListener(BackToMainMenu);
    }

    private void OnDisable() {
        nextLevelButton.onClick.RemoveListener(NextLevel);
        retryButton.onClick.RemoveListener(RetryLevel);
        mainmenuButton.onClick.RemoveListener(BackToMainMenu);
    }

    private void BackToMainMenu() {
        GameManager.Instance.ChangeState(GameState.MainMenu);
    }

    private void RetryLevel() {
        GameManager.Instance.OnRetryLevel();
    }

    private void NextLevel() {
        GameManager.Instance.OnNextLevel();
    }


}
