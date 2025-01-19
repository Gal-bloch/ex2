using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab; // Prefab to spawn when dragging
    private GameObject spawnedItem; // Instance of the spawned item
    private bool isDragging = false;

    void OnMouseDown()
    {
       // Debug.Log("mOUSE");
        // Spawn the item at the cursor position
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnedItem = Instantiate(itemPrefab, mousePosition, itemPrefab.transform.rotation);
        isDragging = true;
        // disable sprite renderer and of children
        GetComponent<SpriteRenderer>().enabled = false;
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            sr.enabled = false;
        }
    }

    void OnMouseDrag()
    {
        if (isDragging && spawnedItem != null)
        {
            // Update the position of the spawned item to follow the cursor
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnedItem.transform.position = mousePosition;
        }
    }

    void OnMouseUp()
    {
        if (isDragging && spawnedItem != null)
        {
            // Stop dragging and activate the item
            isDragging = false;
            ActivateItem(spawnedItem);
            Destroy(gameObject);
        }
    }

    private void ActivateItem(GameObject item)
    {
       // search for a script that implements the Activable interface
       //     and call activateItem on it
       
        
        Activable activable = item.GetComponent<Activable>();
        if (activable != null)
        {
            activable.ActivateItem();
        }
    }
}
