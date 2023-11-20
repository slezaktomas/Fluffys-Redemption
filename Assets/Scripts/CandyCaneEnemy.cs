using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCaneEnemy : MonoBehaviour
{
    private Transform target; // Transformace hráče
    [SerializeField] private float moveSpeed = 3.0f; // Rychlost pohybu
    [SerializeField] private float dashRange = 5.0f; // Vzdálenost, ve které začne nepřítel útočit
    [SerializeField] private float dashSpeed = 6.0f; // Rychlost bleskového útoku
    [SerializeField] private float dashCooldown = 2.0f; // Doba mezi bleskovými útoky
    [SerializeField] private float dashPauseTime = 1.0f; // Doba pozastavení po bleskovém útoku

    private bool isDashing = false; // Indikuje, zda nepřítel právě provádí bleskový útok
    private float dashCooldownTimer = 0.0f; // Časovač pro bleskový útok

    private void Start()
    {
        // Najde transformaci hráče na základě značky "Player".
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (target == null)
        {
            // Hráč nebyl nalezen; sem můžete přidat potřebnou obsluhu.
            return;
        }

        // Spočítá směr, kterým se bude pohybovat směrem k hráči.
        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer <= dashRange && !isDashing && dashCooldownTimer <= 0)
        {
            // Zahájí bleskový útok směrem k hráči.
            StartDashTowardsPlayer(direction);
        }
        else
        {
            // Pohybuje se běžnou rychlostí.
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            if (dashCooldownTimer > 0)
            {
                dashCooldownTimer -= Time.deltaTime;
            }
        }
    }

    private void StartDashTowardsPlayer(Vector3 direction)
    {
        if (!isDashing)
        {
            isDashing = true;
            StartCoroutine(DashTowardsPlayer(direction));
        }
    }

    private IEnumerator DashTowardsPlayer(Vector3 direction)
    {
        float dashDistance = 2.0f;
        float distance = 0;

        while (distance < dashDistance)
        {
            transform.Translate(direction * dashSpeed * Time.deltaTime);
            distance += dashSpeed * Time.deltaTime;
            yield return null;
        }

        // Pozastavení na zadanou dobu po bleskovém útoku.
        yield return new WaitForSeconds(dashPauseTime);

        isDashing = false;
        dashCooldownTimer = dashCooldown;
    }
}
