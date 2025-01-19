using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingedSwing : MonoBehaviour, Activable
{
    public bool isHeld { get; set; }

    private bool isBaloon = false;

    public void putBaloon(bool should, string side)
    {
        isBaloon = should;

        // Check for Rigidbody and destroy it if present
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Destroy(rb);
        }

        // Determine rotation angle based on side
        float rotationAngle = side.ToLower() == "left" ? 37f : side.ToLower() == "right" ? -37f : 0f;
        if (rotationAngle == 0f)
        {
            Debug.LogWarning("Invalid side specified. Use 'left' or 'right'.");
            return;
        }

        StartCoroutine(RotateOverTime(rotationAngle));

        // Search for a child named "Baloon" and rotate it
        Transform baloon = transform.Find("Baloon");
        if (baloon != null)
        {
            StartCoroutine(RotateChildOverTime(baloon, -rotationAngle));
        }
        else
        {
            Debug.LogWarning("No child object named 'Baloon' found.");
        }
    }

    private IEnumerator RotateOverTime(float targetAngle)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, targetAngle));
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
    }

    private IEnumerator RotateChildOverTime(Transform child, float targetAngle)
    {
        Quaternion startRotation = child.localRotation;
        Quaternion endRotation = Quaternion.Euler(child.localEulerAngles + new Vector3(0, 0, targetAngle));
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            child.localRotation = Quaternion.Lerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        child.localRotation = endRotation;
    }

    public void ActivateItem()
    {
        Debug.Log("Activating " + name);
    }
    
    public void OnPickup()
    {
        //
    }

}
