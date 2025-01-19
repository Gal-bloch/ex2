using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel1 : MonoBehaviour
{
    public GameObject[] things_to_activate;
    public GameObject[] things_to_deactivate;

    private void showCutScene()
    {
        foreach (GameObject thing in things_to_activate)
        {
            thing.SetActive(true);
        }
        foreach (GameObject thing in things_to_deactivate)
        {
            thing.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Dodo"))
        {
            showCutScene();
        }
    }
}
