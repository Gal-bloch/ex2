using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baloon : MonoBehaviour, Activable
{
    
    public bool isHeld { get; set; }

    private bool attached = false;
    private float radius = 0.5f;
    private Quaternion initialRotation;

    void Start()
    {
        // Store the initial rotation of the object
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // Override the object's rotation to the stored initial rotation
        transform.rotation = initialRotation;
    }
    public void ActivateItem(){
       // sphere ray case with radius parameteized from the son of gameobject "Sensor"
        //if it hits a collider with the tag "Plank" it will call the method putBaloon(true) on the object
        // only allow one plank to be balooned at a time
        
        Transform sensor = transform.Find("sensor");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(sensor.position, radius);

        Debug.Log("Amount of colliders: " + colliders.Length);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Plank")
            {
                Plank plank = collider.GetComponent<Plank>();
                if (plank != null)
                {
                    plank.putBaloon(true);
                    attached = true;
                    break;
                }
            }
            if (collider.tag == "WingedSwing")
            {
                WingedSwing wingedSwing = collider.GetComponentInParent<WingedSwing>();
                if (wingedSwing != null)
                {
                    transform.parent = wingedSwing.transform;
                    wingedSwing.putBaloon(true, "left");
                    attached = true;
                    break;
                }
            }
            
        }
        
        if (!attached)
        {
            Debug.Log("Not Attached");
            Destroy(gameObject);
        }
    }
    
    public void OnPickup()
    {
        //
    }

}
