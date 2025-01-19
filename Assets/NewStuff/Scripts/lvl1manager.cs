using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lvl1manager : MonoBehaviour
{
    // Singleton
    public static lvl1manager instance;
    public GameObject trapChallengePrefab;
    public TrapSystem trapSystem;
    public GameObject dodo;
    public Transform dodoFailPosition;

    public Transform checkpoint;
    private int life = 3;
    public TextMeshProUGUI lifeText;

    public GameObject reg_cam;
    public GameObject boss_cam;

    public Entity entity;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void resetBossFight()
    {
        if (trapSystem == null || trapSystem.transform.parent == null)
        {
            Debug.LogError("Trap system or its parent is missing!");
            return;
        }

        // Store the position of the trap system parent
        Vector3 pos = trapSystem.transform.parent.position;

        // Destroy the current trap system and its parent
        Destroy(trapSystem.transform.parent.gameObject);

        // Instantiate a new trap challenge at the same position
        GameObject trapChallenge = Instantiate(trapChallengePrefab, pos, Quaternion.identity);

        // Access the new TrapSystem component from the child
        trapSystem = trapChallenge.transform.GetChild(0).GetComponent<TrapSystem>();

        if (trapSystem == null)
        {
            Debug.LogError("Failed to find TrapSystem component in the new trap challenge.");
            return;
        }

       
        dodo.SetActive(true);
        lvl1manager.instance.ActivateCam(false);
        DodoDied();
        Hunter.counter = 0;

    }

    public void ActivateCam(bool should)
    {
        boss_cam.SetActive(should);
        reg_cam.SetActive(!should);

        // Set the mainCamera to the appropriate Camera component based on the "should" flag
        entity.mainCamera = should ? boss_cam.GetComponent<Camera>() : reg_cam.GetComponent<Camera>();
    }

    public void DodoDied()
    {
        Debug.Log("Happened");
        life--;
        if (lifeText != null)
        {
            lifeText.text = life.ToString();
        }

        if (life <= 0)
        {
            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (dodo != null && checkpoint != null)
        {
            dodo.transform.position = checkpoint.position;
        }
        else
        {
            Debug.LogError("Dodo or checkpoint is missing!");
        }
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
