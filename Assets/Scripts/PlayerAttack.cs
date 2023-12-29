using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask EnemyLayerMask;
    public float attackRange;
    public float moveDistance;
    public float normalMoveSpeed = 5f;
    public float attackMoveSpeed = 10f;

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private IEnumerator AttackAndMove()
    {
        float originalMoveSpeed = normalMoveSpeed;
        normalMoveSpeed = attackMoveSpeed;

        Attack();
        //MovePlayer();
        yield return new WaitForSeconds(startTimeBtwAttack);
        normalMoveSpeed = originalMoveSpeed;
    }

    private void Attack()
    {
        Collider[] enemiesToDamage = Physics.OverlapSphere(attackPos.position, attackRange, EnemyLayerMask);
        
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if (enemiesToDamage[i].GetComponent<Enemy>())
            {
                Debug.Log("We hit " + enemiesToDamage[i].name);
                enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(Player.Damage);
            }
            if (enemiesToDamage[i].GetComponent<Boss>())
            {
                enemiesToDamage[i].GetComponent<Boss>().TakeDamage(Player.Damage);
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    /*private void MovePlayer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = hit.point;
            targetPosition.y = transform.position.y;
            Vector3 direction = (targetPosition - transform.position).normalized;
            direction = EightDirections(direction);
            targetPosition = transform.position + direction * moveDistance;
            StartCoroutine(MovePlayerCoroutine(targetPosition));
        }
    }

    private Vector3 EightDirections(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        int octant = Mathf.RoundToInt(angle / 45.0f);
        float snappedAngle = octant * 45.0f;
        float correctedX = Mathf.Cos(snappedAngle * Mathf.Deg2Rad);
        float correctedZ = Mathf.Sin(snappedAngle * Mathf.Deg2Rad);

        return new Vector3(correctedX, 0, correctedZ);
    }

    private IEnumerator MovePlayerCoroutine(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, normalMoveSpeed * Time.deltaTime);
            yield return null;
        }
    }*/
}
