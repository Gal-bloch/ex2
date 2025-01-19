using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dodobird : MonoBehaviour
{
    [SerializeField] float speed = 3f; // Movement speed
    private Rigidbody2D rb;
    private bool isMoving = false;

    void Awake()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Apply movement only if isMoving is true
        if (isMoving)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // Stop horizontal movement
        }
    }

    public void startMove()
    {
        // Start moving right
        isMoving = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the "Exit" tag
        if (collision.gameObject.CompareTag("Exit"))
        {
            string nextSceneName = GetNextSceneName();

            // Attempt to load the next scene
            if (!string.IsNullOrEmpty(nextSceneName) && Application.CanStreamedLevelBeLoaded(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogWarning($"Scene '{nextSceneName}' could not be loaded. Ensure it exists and is added to the build settings.");
            }
        }
        if (collision.gameObject.CompareTag("Fire"))
        {
            // Restart the level
            RestartLevel();
        }
    }
    
    private void RestartLevel()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private string GetNextSceneName()
    {
        // Get the current scene's name
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Extract the numeric part of the scene name
        string baseName = "";
        int levelNumber = 0;

        for (int i = 0; i < currentSceneName.Length; i++)
        {
            if (char.IsDigit(currentSceneName[i]))
            {
                levelNumber = levelNumber * 10 + (currentSceneName[i] - '0');
            }
            else
            {
                baseName += currentSceneName[i];
            }
        }

        // Increment the level number and construct the next scene's name
        return baseName + (levelNumber + 1);
    }
}
