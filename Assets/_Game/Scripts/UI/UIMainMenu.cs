using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UICanvas
{
    [SerializeField] private Button playButton;
    [SerializeField] private TMP_Text currentLevelText;

    private void OnEnable() {
        playButton.onClick.AddListener(PlayGame);
        currentLevelText.SetText($"Level {GameManager.Instance.currentLevel + 1}");
    }

    private void OnDisable() {
        playButton.onClick.RemoveListener(PlayGame);
    }

    private void PlayGame() {
        GameManager.Instance.ChangeState(GameState.GamePlay);
    }

    
}
