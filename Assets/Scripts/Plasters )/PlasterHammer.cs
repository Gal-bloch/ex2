using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasterHammer : MonoBehaviour
{
    public Rigidbody2D hammer_rb;

    public void LetLoose()
    {
        hammer_rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
