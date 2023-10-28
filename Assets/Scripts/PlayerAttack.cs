using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask EnemyLayerMask;
    public float attackRange;

    private void Update()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, EnemyLayerMask);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (enemiesToDamage[i].GetComponent<Enemy>())
                    {
                        enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(Player.Damage);
                    }
                    if (enemiesToDamage[i].GetComponent<Boss>())
                    {
                        enemiesToDamage[i].GetComponent<Boss>().TakeDamage(Player.Damage);
                    }
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position,attackRange);
    }
}
