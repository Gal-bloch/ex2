using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftItem : MonoBehaviour, Activable
{
    public bool isHeld { get; set; }
    public string item_name;

    public void ActivateItem()
    {
        // Define the circle cast parameters
        float circleRadius = 2.0f; // Adjust as needed
        Vector2 origin = transform.position;

        // Perform the circle cast to find all nearby colliders
        Collider2D[] colliders = Physics2D.OverlapCircleAll(origin, circleRadius);

        foreach (Collider2D collider in colliders)
        {
            // Check if the collider has a Recipe component
            Recipe recipe = collider.GetComponent<Recipe>();
            if (recipe != null && recipe.item_name == item_name)
            {
                // Call supplyRecipe method to mark as crafted or process the recipe
                recipe.supplyRecipe();

                // Destroy the crafted item
                Destroy(gameObject);
                Debug.Log($"{item_name} crafted successfully!");
                return; // Exit after crafting the first matching item
            }
        }
        

        // Set the Rigidbody2D to dynamic if available
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void OnPickup()
    {
        // Logic for when the item is picked up
        isHeld = true;
        Debug.Log($"{item_name} picked up!");
    }
}