using UnityEngine;

public class CandyCaneEnemy : MonoBehaviour
{
    private Transform target;
    public Rigidbody rb;
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float dashForce = 10.0f;
    private bool canDash = false;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
            MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit by enemy");
            DealDamageToPlayer();
        }
    }

    private void DealDamageToPlayer()
    {
        GameObject heartObject = GameObject.FindGameObjectWithTag("Hearts");
        if (heartObject != null)
        {
            HurtPlayer hurtPlayerScript = heartObject.GetComponent<HurtPlayer>();
            if (hurtPlayerScript != null)
            {
                hurtPlayerScript.Hurt();
            }
        }
    }
}
