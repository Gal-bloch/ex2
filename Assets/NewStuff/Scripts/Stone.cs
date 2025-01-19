using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour, Activable
{
    private bool canKill = false;
    public float threshold = 3f;
    public bool isHeld { get; set; }

    public float minTimeToKill = 0.1f; // Minimum time before stone can kill
    private float timer = 0f;

    public void ActivateItem()
    {
        // Debug.Log("Activating " + name);
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        canKill = true;
        timer = minTimeToKill; // Start the timer when activated

        GetComponent<CircleCollider2D>().radius = 2f;
    }

    public void OnPickup()
    {
        // Reset the stone when picked up
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        canKill = false;
        timer = 0f; // Reset the timer when picked up
    }

    void Update()
    {
        // Decrease the timer only if it's active (i.e., stone is in Dynamic mode)
        if (canKill && timer > 0)
        {
            timer -= Time.deltaTime; // Decrease timer by the time elapsed in each frame
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object's layer matches "Ground"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Debug.Log("Stone collided with ground");
            canKill = false;
            GetComponent<CircleCollider2D>().radius = 3f;
        }

        // Check if the stone collided with a "Hunter"
        if (collision.gameObject.CompareTag("Hunter"))
        {
            // Allow killing only if the stone has been active for the required time
            if (canKill && timer <= 0)
            {
                lvl1manager.instance.trapSystem.decreaseAmount(collision.gameObject);
                collision.gameObject.GetComponent<Hunter>().killHunter();
                Destroy(gameObject);
                lvl1manager.instance.trapSystem.decreaseAmount(gameObject);
            }
        }
    }
}
