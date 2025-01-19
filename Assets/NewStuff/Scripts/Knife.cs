using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour, Activable
{
    
    public bool isHeld { get; set; }
    public float castRadius = 1f;
    public Transform tip;
    public void ActivateItem(){
        Debug.Log("Activating " + name);
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(tip.position, castRadius);

        Debug.Log("Amount of colliders: " + colliders.Length);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("NetRope"))
            {
                Destroy(collider.gameObject);
                Destroy(gameObject);
                lvl1manager.instance.trapSystem.cutRope();
                return;
            }
        }

        
        
    }
    
    public void OnPickup()
    {
        //set rotation to identity
        transform.rotation = Quaternion.identity;
    }
    
    


}