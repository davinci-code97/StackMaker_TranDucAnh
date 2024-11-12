using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlaying : UICanvas
{
    [SerializeField] private Button settingButton;
    [SerializeField] private TMP_Text currentLevelText;

    void OnEnable()
    {
        settingButton.onClick.AddListener(OpenSetting);
        currentLevelText.SetText($"Level {GameManager.Instance.currentLevel + 1}");
    }

    private void OnDisable() {
        settingButton.onClick.RemoveListener(OpenSetting);
    }

    private void OpenSetting() {
        GameManager.Instance.ChangeState(GameState.Setting);
    }


}
