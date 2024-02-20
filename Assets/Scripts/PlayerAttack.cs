using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask EnemyLayerMask;
    private float attackRange;
    public float moveDistance;
    public float normalMoveSpeed = 5f;
    public float attackMoveSpeed = 10f;
    private Animator anim;
    public Light light;
    public string name;
    public static PlayerAttack Instance { get; private set; }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        Instance = this;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetMouseButtonDown(0) )
            {
                Debug.Log(name);
                
                if (name.Trim() == "RosyGazeblade")
                {
                    anim.SetBool("Sword", true);
                }
                if (name == "Fluffkiri")
                {
                    anim.SetBool("Katana", true);
                }
                
                StartCoroutine(AttackAndMove());
                timeBtwAttack = startTimeBtwAttack;
            }
            else
            {
                anim.SetBool("Katana", false);
                anim.SetBool("Sword", false);
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
                enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(Player.Damage);
            }
            if (enemiesToDamage[i].GetComponent<Boss>())
            {
                enemiesToDamage[i].GetComponent<Boss>().TakeDamage(Player.Damage);
            }
        }
    }

    public void SetRanges(float newAttackRange, float newLightRange)
    {
        attackRange = newAttackRange;
        light.spotAngle = newLightRange;
    }
    public void SetName(string Name)
    {
        
        name = Name;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
