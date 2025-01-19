using System.Collections;
using UnityEngine;

public class Coconut : MonoBehaviour
{
    [SerializeField] private float rollSpeed = 2f;
    [SerializeField] private LayerMask groundLayer; // This is a bitmask
    private Rigidbody2D rb;

    private bool hitGround = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object's layer is part of the groundLayer mask
        if (!hitGround && ((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            hitGround = true;

            // Determine random roll direction
            float rollDirection = Random.Range(0, 2) == 0 ? -1 : 1;

            // Apply horizontal force to simulate rolling
            rb.velocity = new Vector2(rollDirection * rollSpeed, rb.velocity.y);

            // Destroy coconut after 3 seconds
            StartCoroutine(DestroyAfterSeconds(3f));
        }

        // Check if the coconut hits the Dodo
        if (!hitGround && collision.gameObject.CompareTag("Dodo"))
        {
            //Debug.Log("Hit Dodo!");
            collision.gameObject.GetComponent<Dodo>().smashDodo();
            
        }
    }

    private IEnumerator DestroyAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
       // Destroy(gameObject);   
    }
}