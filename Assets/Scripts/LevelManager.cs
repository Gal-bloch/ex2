using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] activateOnStartItems; // Array of activable objects

    void Update()
    {
        // if pressed r restart the level
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Restart the level
            RestartLevel();
        }
        // if pressed s start the level
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Start the level
            startLevel();
        }
        
    }
    
    public void RestartLevel()
    {
        // Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void startLevel()
    {
        // search for instance of Dodobird in all scene objects
        Dodobird dodobird = FindObjectOfType<Dodobird>();
        dodobird.startMove();
        
        ActivateAll();
        
        
    }
    public void ActivateAll()
    {
        // Loop through all activable objects and activate them
        foreach (GameObject item in activateOnStartItems)
        {
            // search for a script that implements the Activable interface
            //     and call activateItem on it
            Activable activable = item.GetComponent<Activable>();
            if (activable != null)
            {
                activable.ActivateItem();
            }
        }
    }
}
