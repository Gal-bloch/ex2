using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour, Activable
{
    public bool isHeld { get; set; }
    public float destroy_time = 3f;
    public void ActivateItem(){
        Debug.Log("Activating " + name);
        gameObject.AddComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // if collides with ground layer destroy after 1 second
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
           // Destroy(gameObject, destroy_time);
        }
    }
    
    public void OnPickup()
    {
        // set rotation to (0,0,60)
        transform.rotation = Quaternion.Euler(0, 0, -60);
        if (gameObject.GetComponent<Rigidbody2D>() != null)
        {
            Destroy(gameObject.GetComponent<Rigidbody2D>());
        }
    }

}
