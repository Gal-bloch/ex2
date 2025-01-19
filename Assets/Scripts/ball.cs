using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public string letter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if the ball collides with the sensor
        if (other.tag == "Sensor")
        {
            // get the last character of the sensor's name
            char index = other.name[other.name.Length - 1];
            // call the setLetter method from the GameManager instance
            GameManager.instance.setLetter(int.Parse(index.ToString()), letter);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // if the ball collides with the sensor
        if (other.tag == "Sensor")
        {
            // get the last character of the sensor's name
            char index = other.name[other.name.Length - 1];
            // call the setLetter method from the GameManager instance
            GameManager.instance.setLetter(int.Parse(index.ToString()), "");
        }
    }
}
