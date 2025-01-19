using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public GameObject SignApplyGameObject;
    
    public void tryApplyRecipe(int kids)
    {
        if (kids <= 1){
            SignApplyGameObject.SetActive(true);
            Destroy(gameObject);
            return;
        }
    }
}
