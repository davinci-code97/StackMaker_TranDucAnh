using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private Brick bridgeBrick;
    [SerializeField] private Collider bridgeCollider;

    public void AddBrickToBridge() {
        bridgeBrick.gameObject.SetActive(true);
        bridgeCollider.enabled = false;
    }


}
