using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    public string item_name;
    public Sign sign;
    public void supplyRecipe()
    {
        sign.tryApplyRecipe(transform.parent.childCount);
        Destroy(gameObject);
        
    }
}
