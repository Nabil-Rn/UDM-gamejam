using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;  // Set to 1 to move exactly one square per tap
    public float cellSize = 1f;   // The size of each square in the grid
    public float inputCooldown = 0.3f;  // Time in seconds to wait before next move
    public LayerMask wallLayer;  // Layer mask to define walls
    public Mazegen mazegen;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private float lastInputTime = -1f;  // Time when the player last pressed a key
    public InputActionAsset inputActions;
    private InputAction moveAction;
    private Stack<Vector2> moveHistory = new();
    private bool hasWon = false;  // Flag to check if the player has won
    void Awake()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        moveHistory.Push(rb.position);
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is missing from the player GameObject!");
            return;
        }

        // Get input actions
        if (inputActions == null)
        {
            Debug.LogError("Input Actions Asset is not assigned in the Inspector!");
            return;
        }

        var playerActionMap = inputActions.FindActionMap("Player");
        if (playerActionMap == null)
        {
            Debug.LogError("Action Map 'Player' not found in Input Actions asset!");
            return;
        }

        moveAction = playerActionMap.FindAction("Move");
        if (moveAction == null)
        {
            Debug.LogError("Move action not found in 'Player' action map!");
            return;
        }
    }

    void OnEnable()
    {
        moveAction?.Enable();
    }

    void OnDisable()
    {
        moveAction?.Disable();
    }

    void Update()
    {
        // Only process input if the cooldown time has passed
        if (Time.time - lastInputTime >= inputCooldown && !hasWon)
        {
            moveInput = moveAction.ReadValue<Vector2>();
            if (moveInput != Vector2.zero) {
                HandleMovement();
            }
        }
    }

    void HandleMovement()
    {
        // Determine the direction to move (one square per tap)
        Vector2 move = Vector2.zero;

        if (moveInput.x > 0)  // Right
            move = Vector2.right * cellSize;
        else if (moveInput.x < 0)  // Left
            move = Vector2.left * cellSize;
        else if (moveInput.y > 0)  // Up
            move = Vector2.up * cellSize;
        else if (moveInput.y < 0)  // Down
            move = Vector2.down * cellSize;

        // Only move if the destination cell is not a wall
        if (move != Vector2.zero)
        {
            // Calculate the target position
            Vector3 targetPosition = rb.position + move;

            // Check if the target position is inside the maze boundaries
            if (IsWithinBounds(targetPosition))
            {
                // Cast a ray to check if there's a collision in the direction of movement
                RaycastHit2D hit = Physics2D.Raycast(rb.position, move, cellSize, wallLayer);

                // If the ray hits something, check if it's not a wall (using LayerMask)
                if (hit.collider == null)  // No hit, or hit something not in the "wallLayer"
                {
                    if (moveHistory.Peek() == rb.position + move)
                    {
                        mazegen.mazePathManager.MakeBack(rb.position);
                        moveHistory.Pop();
                    }
                    else
                    {
                        mazegen.mazePathManager.MakePath(rb.position);
                        moveHistory.Push(rb.position);
                    }
                    // Move the player to the target position if no collision or non-wall object is detected
                    rb.MovePosition(targetPosition);

                    // Update the last input time to prevent immediate re-input
                    lastInputTime = Time.time;
                    CheckForWin((int)targetPosition[1]);
                }
            }
        }
    }

    // Method to check if the target position is within maze bounds
    bool IsWithinBounds(Vector3 targetPosition)
    {
        // Get the current maze boundaries
        float minX = 0;
        float maxX = (mazegen.maze_width - 1) * cellSize;  // maxX is at the far right edge of the maze
        float minY = -(mazegen.maze_height - 1) * cellSize;  // minY is at the bottom edge (negative y in Unity)
        float maxY = 0;  // maxY is at the top edge (y = 0 in the grid)

        // Check if the target position is within these boundaries
        if (targetPosition.x >= minX && targetPosition.x <= maxX &&
            targetPosition.y >= minY && targetPosition.y <= maxY)
        {
            return true;  // The position is within bounds
        }

        return false;  // The position is out of bounds
    }

        // Check if the player has reached the win position
    void CheckForWin(int pos)
    {
        if (pos == -mazegen.maze_height + 1)
        {
            hasWon = true;
            TriggerWin();
        }
    }

    void TriggerWin()
    {
        Debug.Log("You Win!");
        // You can load a new scene or show a UI here
        // For example: 
        // SceneManager.LoadScene("WinScene");
        // Or display a UI: 
        // winUI.SetActive(true);  // If you have a UI to show
    }
}
