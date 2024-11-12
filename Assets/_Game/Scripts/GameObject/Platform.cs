using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Brick platformBrick;
    [SerializeField] private Collider platformCollider;

    public void RemovePlatformBrick() {
        platformCollider.enabled = false;
        platformBrick.gameObject.SetActive(false);
    }

}
