using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public LayerMask[] interactableLayers; // Array of individual layer masks
    private bool isFollowing = false;
    private bool isDragging = false;
    private float mouseDownTime = 0f;
    private const float dragThreshold = 0.2f; // Time in seconds to consider a drag
    private Vector3 offset;
    private GameObject selectedObject;
    public Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            if (!isFollowing)
            {
                if (TrySelectObject())
                {
                    mouseDownTime = Time.time; // Record the time when the button was pressed
                    isFollowing = true;
                    isDragging = false;
                    
                }
                
            }
            else
            {
                isFollowing = false;
                isDragging = false;
                TryActivateItem(); // Activate item after dragging
            }
        }

        if (Input.GetMouseButton(0)) // Mouse button is being held
        {
            if (!isDragging && Time.time - mouseDownTime > dragThreshold)
            {
                isDragging = true; // Transition to drag mode
            }
        }

        if (Input.GetMouseButtonUp(0)) // Left mouse button released
        {
            if (isDragging)
            {
                isDragging = false; // Stop dragging
                isFollowing = false;
                TryActivateItem(); // Activate item after dragging
            }
        }

        if (isFollowing)
        {
            FollowMousePosition();
        }
    }

    private bool TrySelectObject()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Combine all layers from the array into a single LayerMask
        LayerMask combinedLayerMask = CombineLayers(interactableLayers);

        // Perform raycast
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, combinedLayerMask);

        if (hit.collider != null)
        {
            selectedObject = hit.collider.gameObject;
            isFollowing = true;

            // Call OnPickup if the object is activable
            Activable activable = selectedObject.GetComponent<Activable>();
            if (activable != null)
            {
                activable.OnPickup();
                int newLayer = LayerMask.NameToLayer("Interactable");
                if (newLayer != -1) // Ensure the layer exists
                {
                    selectedObject.layer = newLayer;
                }

                activable.isHeld = true;
            }
        }
        else
        {
            selectedObject = null;
        }
        return selectedObject != null;
    }

    private void FollowMousePosition()
    {
        if (selectedObject != null)
        {
            Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            selectedObject.transform.position = mouseWorldPosition;
        }
       
    }

    private void TryActivateItem()
    {
        if (selectedObject != null)
        {
            Activable activable = selectedObject.GetComponent<Activable>();
            if (activable != null)
            {
                activable.ActivateItem();
                activable.isHeld = false;
            }

            // Stop following the object
            isFollowing = false;
            selectedObject = null;
        }
    }

    // Helper method to combine multiple LayerMasks into one
    private LayerMask CombineLayers(LayerMask[] layers)
    {
        LayerMask combined = 0;
        foreach (LayerMask layer in layers)
        {
            combined |= layer.value;
        }
        return combined;
    }
}
