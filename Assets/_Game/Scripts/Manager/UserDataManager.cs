using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : Singleton<UserDataManager>
{
    private string levelSaveKey = "PlayerLevelSaveKey";

    public void SetLevel(int level) => PlayerPrefs.SetInt(levelSaveKey, level);

    public int GetLevel() => PlayerPrefs.GetInt(levelSaveKey, 0);


}
