using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : MonoBehaviour, Activable
{
    private bool isBaloon = false;
    
    //implement isHeld property
    public bool isHeld { get; set; }
    
    public void putBaloon(bool should)
    {
        isBaloon = should;
        //if rigidbody is not null, destroy it
        if (gameObject.GetComponent<Rigidbody2D>() != null)
        {
           // Destroy(gameObject.GetComponent<Rigidbody2D>());
           Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
           rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
    public void ActivateItem(){
        Debug.Log("Activating " + name);
        //gameObject.AddComponent<BoxCollider2D>();
        if (!isBaloon)
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    
    public void OnPickup()
    {
        //set rotation to identity
        transform.rotation = Quaternion.identity;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object's layer matches "Ground"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Debug.Log("Plank collided with ground");

            // Set layer to "InteractableAndCollidable"
            int newLayer = LayerMask.NameToLayer("InteractableAndCollidable");
            if (newLayer != -1) // Ensure the layer exists
            {
                if (!isHeld)
                {
                    gameObject.layer = newLayer;
                }
                
            }
            else
            {
                Debug.LogWarning("Layer 'InteractableAndCollidable' does not exist!");
            }
        }
    }


}
