using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetDodo : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Reset"))
        {
            Destroy(other.gameObject);
            //resetlevel using scenemanager
           // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
           lvl1manager.instance.resetBossFight();
//           Debug.Log("Got Shot");
        }
    }
}
