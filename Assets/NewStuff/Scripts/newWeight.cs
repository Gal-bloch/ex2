using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newWeight : MonoBehaviour, Activable
{
    public bool isHeld { get; set; }

    private Rigidbody2D rb;

    void Start()
    {
        // Cache the Rigidbody2D component
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void ActivateItem()
    {
        Debug.Log("Activating " + name);

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        // Set layer to default
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void OnPickup()
    {
        // Reset the Rigidbody and rotation
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f; // Stops any rotational movement
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        // Set rotation to identity (reset rotation)
        transform.rotation = Quaternion.identity;

        Debug.Log(name + " picked up and reset.");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object's layer matches "Ground"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Debug.Log("newWeight collided with ground");
            // Set layer to Interactable
            gameObject.layer = LayerMask.NameToLayer("Interactable");
        }
    }
}