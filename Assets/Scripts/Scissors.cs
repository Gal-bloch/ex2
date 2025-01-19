using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors : MonoBehaviour, Activable
{
    public bool isHeld { get; set; }

    public Transform tip; // The point from which the sphere cast will start.
    public float castRadius = 0.5f; // Radius of the sphere cast.

    public void ActivateItem()
    {
        Debug.Log("Activating " + name);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(tip.position, castRadius);

        Debug.Log("Amount of colliders: " + colliders.Length);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "CutableRope")
            {
                collider.GetComponent<HingeJoint2D>().enabled = false;
            }
        }
        
        Destroy(gameObject);
    }
    
    public void OnPickup()
    {
        //
    }
    
}