using System.Collections;
using UnityEngine;

public class CandyCaneEnemy : MonoBehaviour
{
    private Transform target;
    public Rigidbody rb;
    [SerializeField] private float moveSpeed = 3.0f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        // Continuously move towards the player
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        // Move towards the player using Rigidbody.
        rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the enemy collides with the player
        if (other.CompareTag("Player"))
        {
            // Deal damage to the player
            DealDamageToPlayer();
        }
    }

    private void DealDamageToPlayer()
    {
        // Find the player object and access the HurtPlayer script
        GameObject heartObject = GameObject.FindGameObjectWithTag("Hearts");
        if (heartObject != null)
        {
            HurtPlayer hurtPlayerScript = heartObject.GetComponent<HurtPlayer>();

            // Check if the HurtPlayer script is found
            if (hurtPlayerScript != null)
            {
                Debug.Log("Dealing damage to player");
                // Call the method to hurt the player
                hurtPlayerScript.Hurt();
            }
        }
    }
}
