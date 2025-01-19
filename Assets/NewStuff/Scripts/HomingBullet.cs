using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public Transform target; // The target the bullet will home in on (in this case, the Dodo)
    public float speed = 5f; // Speed of the bullet
    public float rotationSpeed = 200f; // Rotation speed to smoothly rotate the bullet toward the target
    public float lifetime = 5f; // Bullet lifetime to destroy it after some time

    private Vector3 dir;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (target != null)
        {
            // Optionally, you can set up a fixed direction to start the bullet, but here we use homing.
            dir = (target.position - transform.position).normalized;
            rb.velocity = dir * speed;
        }

        // Destroy the bullet after 'lifetime' seconds to prevent it from staying forever
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
       /* if (target != null)
        {
            // Calculate the direction to the target
            Vector3 direction = (target.position - transform.position).normalized;

            // Smoothly rotate the bullet toward the target
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Move the bullet in the direction of the target
            rb.velocity = direction * speed;
        }
        */
       rb.velocity = dir * speed;
    }


}