using System.Collections;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public float moveSpeed = 5f;       // Constant movement speed
    public float edgeThickness = 10f; // Edge thickness for mouse drag movement
    public bool lockY = false;        // If true, camera can only move on the X-axis

    private Vector3 movementDirection; // Current movement direction

    public Transform topLeftBound;     // Top-left boundary for clamping
    public Transform bottomRightBound; // Bottom-right boundary for clamping

    // Update is called once per frame
    void Update()
    {
        movementDirection = Vector3.zero; // Reset movement direction at the start of each frame

        HandleKeyboardInput();
        //HandleEdgeScrolling();
        ApplyMovement();

        // Restart the level if 'R' is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        // Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void HandleKeyboardInput()
    {
        // WASD/Arrow Key Movement
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        float vertical = Input.GetAxisRaw("Vertical");     // W/S or Up/Down

        // Only set movement if there's actual input
        movementDirection = new Vector3(horizontal, lockY ? 0 : vertical, 0);
    }

    private void HandleEdgeScrolling()
    {
        Vector3 edgeDirection = Vector3.zero;

        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Check if the mouse is near the edges of the screen
        if (mousePosition.x >= Screen.width - edgeThickness) // Right edge
        {
            edgeDirection.x = 1;
        }
        else if (mousePosition.x <= edgeThickness) // Left edge
        {
            edgeDirection.x = -1;
        }

        if (!lockY) // Allow vertical edge scrolling only if lockY is false
        {
            if (mousePosition.y >= Screen.height - edgeThickness) // Top edge
            {
                edgeDirection.y = 1;
            }
            else if (mousePosition.y <= edgeThickness) // Bottom edge
            {
                edgeDirection.y = -1;
            }
        }

        // If edge scrolling is detected, override current direction
        if (edgeDirection != Vector3.zero)
        {
            movementDirection = edgeDirection;
        }
    }

    private void ApplyMovement()
    {
        if (movementDirection != Vector3.zero)
        {
            // Move the camera, restricting to X-axis if lockY is true
            transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);
        }

        // Clamp the camera to the bounds
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, topLeftBound.position.x, bottomRightBound.position.x),
            lockY ? transform.position.y : Mathf.Clamp(transform.position.y, bottomRightBound.position.y, topLeftBound.position.y),
            transform.position.z
        );
    }
}
