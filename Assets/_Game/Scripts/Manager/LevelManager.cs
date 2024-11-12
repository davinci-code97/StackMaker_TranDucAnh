using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class LevelManager : Singleton<LevelManager> {

    [SerializeField] private List<GameObject> levelPrefabList;
    private GameObject currentMap;
    private Transform currentStartPoint;

    public bool IsFinishGame => GameManager.Instance.currentLevel >= levelPrefabList.Count;

    public void LoadMap(int level) {
        if (level <= levelPrefabList.Count) {
            currentMap = Instantiate(levelPrefabList[level]);
            currentMap.transform.position = Vector3.zero;
            currentStartPoint = currentMap.GetComponent<Level>().StartPoint;
        }
    }

    public void DestroyCurrentMap() {
        if (currentMap == null)
            return;
        Destroy(currentMap);
    }

    public Vector3 GetCurrentStartPoint() {
        return currentStartPoint.localPosition;
    }


}
