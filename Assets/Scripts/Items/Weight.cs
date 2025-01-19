using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight : MonoBehaviour, Activable
{
    public bool isHeld { get; set; }

    public void ActivateItem()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    
    public void OnPickup()
    {
        //
    }

}
