using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Singleton<Player> {
    public enum Direction {
        None,
        Forward,
        Backward,
        Left,
        Right,
    }

    [SerializeField] private GameObject playerAsset;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private Brick brickPrefab;
    [SerializeField] private List<Brick> brickStack = new List<Brick>();
    [SerializeField] private float brickHeight = .06f;

    private Direction direction = Direction.None;
    
    private bool isSwipping = false;
    private Vector2 swipeStart;
    private Vector2 swipeEnd;
    private Vector2 swipeDelta;

    [SerializeField] float raycastRange;
    private RaycastHit raycastHit;
    

    void Start() {
        OnInit();
    }

    private void OnInit() {
        direction = Direction.None;
    }

    void Update() {
        if (!GameManager.Instance.IsState(GameState.GamePlay)) return;
        if (direction == Direction.None) { 
            CheckInput();
        }
        CheckRaycast();
        CheckDirection();
        //Debug.DrawRay(transform.position, transform.forward * raycastRange, Color.red);
    }

    private void CheckDirection() {
        switch (direction) {
            case Direction.None:
                StopMove();
                break;
            case Direction.Forward:
                MoveUp();
                break;
            case Direction.Backward:
                MoveDown();
                break;
            case Direction.Left:
                MoveLeft();
                break;
            case Direction.Right:
                MoveRight();
                break;
            default:
                break;
        }
    }

    private void CheckInput() {
        if (Input.GetMouseButtonDown(0)) {
            swipeStart = Input.mousePosition;
            isSwipping = true;
        }

        if (Input.GetMouseButtonUp(0) && isSwipping) {
            swipeEnd = Input.mousePosition;
            DetectSwipeDirection();
            isSwipping = false;
        }

        if (isSwipping) {
            swipeDelta = (Vector2)Input.mousePosition - swipeStart;
        }
    }

    private void DetectSwipeDirection() {
        swipeDelta = swipeEnd - swipeStart;

        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) {
            // Horizontal
            if (swipeDelta.x > 0) {
                direction = Direction.Right;
                transform.forward = Vector3.right;
            }
            else {
                direction = Direction.Left;
                transform.forward = Vector3.left;
            }
        }
        else {  
            // Vertical
            if (swipeDelta.y > 0) {
                direction = Direction.Forward;
                transform.forward = Vector3.forward;
            }
            else {
                direction = Direction.Backward;
                transform.forward = Vector3.back;
            }
        }
    }

    private void StopMove() {
        transform.Translate(Vector3.zero);
        direction = Direction.None;
    }

    private void MoveLeft() {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void MoveRight() {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void MoveUp() {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void MoveDown() {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void CheckRaycast() {
        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, raycastRange)) {
            CheckWall();
            CheckPlatform();
            CheckBridge();
            CheckGoal();
        }
    }

    private void CheckWall() {
        if (raycastHit.collider.CompareTag(Constants.TAG_WALL)) {
            StopMove();
        }
    }

    private void CheckPlatform() {
        if (raycastHit.collider.CompareTag(Constants.TAG_PLATFORM)) {
            Platform platform = raycastHit.collider.GetComponent<Platform>();
            AddBrickToStack(platform);
        }
    }

    private void CheckBridge() {
        if (raycastHit.collider.CompareTag(Constants.TAG_BRIDGE)) {
            Bridge bridge = raycastHit.collider.GetComponent<Bridge>();
            if (brickStack.Count > 0) {
                BuildBridge(bridge);
            } else {
                StopMove();
            }
        }
    }

    private void CheckGoal() {
        if (raycastHit.collider.CompareTag(Constants.TAG_GOAL)) {
            StopMove();
            ClearAllBricks();
            UpdatePlayerHeight();
            GameManager.Instance.ChangeState(GameState.ClearLevel);
        }
    }

    private void AddBrickToStack(Platform platform) {
        Brick newBrick = Instantiate(brickPrefab, new Vector3(transform.position.x, transform.position.y + brickHeight * brickStack.Count, transform.position.z), Quaternion.identity, transform);
        brickStack.Add(newBrick);
        // remove brick on platform
        platform.RemovePlatformBrick();

        UpdatePlayerHeight();
    }
    
    private void BuildBridge(Bridge bridge) {
        RemoveTopBrick();
        bridge.AddBrickToBridge();

        UpdatePlayerHeight();
    }

    private void RemoveTopBrick() {
        Brick topBrick = brickStack.Last();
        Destroy(topBrick.gameObject);
        brickStack.Remove(topBrick);
    }

    public void ClearAllBricks() {
        foreach (Brick brick in brickStack) {
            Destroy(brick.gameObject);
        }
        brickStack.Clear();
        UpdatePlayerHeight();
    }

    private void UpdatePlayerHeight() {
        playerAsset.transform.position = new Vector3(transform.position.x, transform.position.y + brickHeight * brickStack.Count, transform.position.z);
    }


}
