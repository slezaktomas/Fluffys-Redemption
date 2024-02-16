using System;
using UnityEngine;
using System.Collections;

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
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

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
                anim.SetBool("Attack",true);
                StartCoroutine(AttackAndMove());
                timeBtwAttack = startTimeBtwAttack;
            }
            else
            {
                anim.SetBool("Attack",false);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
