using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dodo : MonoBehaviour
{
    public float speed = 1.0f; // Speed of the dodo's movement
    private float initial_speed;
    private Rigidbody2D rb;   // Reference to the Rigidbody2D

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component attached to the dodo
        rb = GetComponent<Rigidbody2D>();
        
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing!");
        }

        initial_speed = speed;
    }

    // FixedUpdate is called at a consistent interval (better for physics)
    void FixedUpdate()
    {
        if (rb != null)
        {
            // Move the dodo continuously to the right
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }

    private IEnumerator returnSpeed()
    {
        yield return new WaitForSeconds(3f); // Wait for 1 second
        speed = initial_speed;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Swing"))
        {
            speed = initial_speed * 2;
            StartCoroutine(returnSpeed()); // Start the coroutine
        }
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
        if (collision.gameObject.CompareTag("Reset"))
        {
            // Restart the level
            //RestartLevel();
            lvl1manager.instance.DodoDied();
        }

        if (collision.gameObject.CompareTag("Trap"))
        {
            gameObject.SetActive(false);
            collision.gameObject.GetComponent<Animator>().SetTrigger("TriggerTrap");
            collision.gameObject.GetComponent<TrapSystem>().enabled = true;
            lvl1manager.instance.ActivateCam(true);
        }
        
        if (collision.gameObject.CompareTag("Swing"))
        {
            speed = initial_speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            lvl1manager.instance.checkpoint = other.transform;
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

    public void smashDodo()
    {
        Debug.Log("Dodo Smashed!");
        //RestartLevel();
        lvl1manager.instance.DodoDied();
    }
}