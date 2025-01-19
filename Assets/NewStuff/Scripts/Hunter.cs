using System.Collections;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    /*
     * I want the hunter to shoot every 8-10 seconds
     * I want the hunter to blink randomly right or left float paramterized distance
     * every 5-10 seconds (only if not in the process of shooting which takes 2 seconds
     * I want it to tend to return the hunter to its original position as a function of how
     * far the hunter is from its original position ( for example if the hunter is 2 distance units
     * from center the odds of it moving back to center are smaller then if it was 5 distance units)
     */
    public float minShootInterval = 8f;
    public float maxShootInterval = 10f;
    public float minDashInterval = 5f;
    public float maxDashInterval = 10f;
    public float dashDistance = 2f; // Parameterized distance for dashes
    public float dashDuration = 1f; // Time it takes to complete a dash
    public AnimationCurve dashCurve; // Custom easing curve
    public Vector3 originalPosition;
    public float centerReturnProbabilityFactor = 0.2f; // Probability multiplier for returning to center

    public float bulletSpeed = 5f; // Adjust speed as needed
    public GameObject bulletPrefab;
    public Transform tip;
    
    public GameObject knifePrefab;

    public static int counter = 0;
    void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(ShootingRoutine());
        StartCoroutine(DashRoutine());
    }

    public IEnumerator Shoot()
    {

        // color red
        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(2f);
        
        // Instantiate the bullet at the tip's position
        GameObject bullet = Instantiate(bulletPrefab, tip.position, Quaternion.identity);

        bullet.GetComponent<HomingBullet>().target = lvl1manager.instance.trapSystem.GetNetDodo();
        // color red
        GetComponent<SpriteRenderer>().color = Color.white;

        

    }

    private IEnumerator ShootingRoutine()
    {
        while (true)
        {
            float shootInterval = Random.Range(minShootInterval, maxShootInterval);
            yield return new WaitForSeconds(shootInterval);
            StartCoroutine("Shoot");

        }
    }

    private IEnumerator DashRoutine()
    {
        while (true)
        {
            float dashInterval = Random.Range(minDashInterval, maxDashInterval);
            yield return new WaitForSeconds(dashInterval);

            
            // Decide whether to move back to center or dash randomly
            float distanceFromCenter = Vector3.Distance(transform.position, originalPosition) / dashDistance;
            float returnToCenterProbability = Mathf.Clamp01(distanceFromCenter * centerReturnProbabilityFactor);

            Vector3 targetPosition;

            if (Random.value < returnToCenterProbability)
            {
                // Dash closer to the original position
                
                Vector3 directionToCenter = (originalPosition - transform.position).normalized;
                targetPosition = transform.position + directionToCenter * dashDistance;
            }
            else
            {
                // Dash randomly left or right
                float dashDirection = Random.value > 0.5f ? 1f : -1f;
                targetPosition = transform.position + new Vector3(dashDirection * dashDistance, 0, 0);
            }

            yield return StartCoroutine(DashToPosition(targetPosition));
            
        }
    }

    private IEnumerator DashToPosition(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / dashDuration;

            // Apply easing
            float easedT = dashCurve.Evaluate(t);

            // Interpolate position
            transform.position = Vector3.Lerp(startPosition, targetPosition, easedT);
            yield return null;
        }

        transform.position = targetPosition;
    }
    
    public void killHunter()
    {
        Destroy(gameObject);
        counter++;
        // 1 of 2 times, spawn a knife
        if ((counter % 3 == 0) && counter > 2 )//(Random.Range(0, 3) == 0 && lvl1manager.instance.trapSystem.canSpawnKnife)
        {
            // Spawn a knife
            GameObject knife = Instantiate(knifePrefab, transform.position, Quaternion.identity);
            knife.transform.SetParent(transform.parent.transform);
          //  knife.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        }
        
    }
}
