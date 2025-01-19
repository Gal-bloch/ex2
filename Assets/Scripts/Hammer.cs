using System.Collections;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public float nailTime = 1f;
    public Transform nail_end;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected");
        
        if (collision.gameObject.name == "nail")
        {
            Transform nail = collision.gameObject.transform;

            // Set the nail as a child of nail_end and start the lerp
            nail.SetParent(nail_end);
            StartCoroutine(LerpNail(nail));

            // Find and destroy the "Tip" child of the nail
            foreach (Transform child in nail)
            {
                if (child.name == "Tip")
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    private IEnumerator LerpNail(Transform nail)
    {
        Vector3 startPosition = nail.localPosition;
        Quaternion startRotation = nail.localRotation;
        Vector3 targetPosition = Vector3.zero;
        Quaternion targetRotation = Quaternion.identity;

        float elapsed = 0f;

        while (elapsed < nailTime)
        {
            elapsed += Time.deltaTime;
            float tFactor = Mathf.Clamp01(elapsed / nailTime);

            nail.localPosition = Vector3.Lerp(startPosition, targetPosition, tFactor);
            nail.localRotation = Quaternion.Lerp(startRotation, targetRotation, tFactor);

            yield return null;
        }

        // Ensure the nail is fully aligned with nail_end
        nail.localPosition = targetPosition;
        nail.localRotation = targetRotation;
    }
}