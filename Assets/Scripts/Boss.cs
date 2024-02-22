using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float Hp;
    private Image healthBar;
    private Image easeBar;
    private float timer = 0.5f;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int bulletPoolSize = 30;
    private List<GameObject> bulletPool = new List<GameObject>();
    [SerializeField] private float shootingSpeed = 8f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private BoxCollider spawnArea;
    [SerializeField] private GameObject alert;

    private bool isAlerted = false;
    private float shootCooldown = 1.5f;
    private float shootTimer = 0f;
    private float distance;
    private bool hasSpawnedEnemies = false;

    private void Start()
    {
        healthBar = UIManager2.Instance.bossHealthBar?.GetComponent<Image>();
        easeBar = UIManager2.Instance.easeHealthBar?.GetComponent<Image>();
        
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    private void Update()
    {
        if (Hp <= 0f)
        {
            Destroy(gameObject);
            SpawnBoss.hasDefeatedBoss = true;
        }
        
        if (Hp >= 40)
        {
            FirstStage();
        }
        if (Hp >= 20 && Hp < 40)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
            StartCoroutine(ShootCircleEveryTwoSeconds());
        }
        
        if (Hp > 0 && Hp < 20)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            ThirdStage();
        }
        
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (healthBar.fillAmount < easeBar.fillAmount)
            {
                float shrinkSpeed = 0.25f;
                easeBar.fillAmount -= shrinkSpeed * Time.deltaTime;
            }
        }
        
    }

    void FirstStage()
    {
        if (player != null)
        {
            distance = Vector3.Distance(transform.position, player.position);
            if (distance >= detectionRange)
            {
                isAlerted = true;
            }
            else
            {
                isAlerted = false;
            }
        }

        if (isAlerted)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                Shoot();
                shootTimer = shootCooldown;
            }
        }
    }
    IEnumerator AlertEnable()
    {
        yield return new WaitForSeconds(1f);
        alert.SetActive(true);
    }
    
    float currentAngle = 0f;
    bool isShooting = false;
    
    IEnumerator ShootCircleEveryTwoSeconds()
    {
        while (true)
        {
            if (!isShooting)
            {
                isShooting = true;
                SecondStage();
                yield return new WaitForSeconds(2f);
                isShooting = false;
            }
            else
            {
                yield return null;
            }
        }
    }

    void SecondStage()
    {
        
        int numberOfProjectiles = 10;
        float angleStep = 360f / numberOfProjectiles;
        float circleRadius = detectionRange * 1.5f;
    
        currentAngle += angleStep;
        StartCoroutine(AlertEnable());

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = currentAngle + (angleStep * i);
            float x = Mathf.Sin(angle * Mathf.Deg2Rad) * circleRadius;
            float z = Mathf.Cos(angle * Mathf.Deg2Rad) * circleRadius;
            Vector3 direction = new Vector3(x, 0f, z);
            GameObject bullet = GetPooledBullet();
            if (bullet != null)
            {
                bullet.transform.position = shootingPoint.position;
                bullet.SetActive(true);

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.velocity = direction.normalized * 10;
            }
        }
        alert.SetActive(false);
    }

    void ThirdStage()
    {
        StartCoroutine(ShootCircleEveryTwoSeconds());
        if (!hasSpawnedEnemies)
        {
            Vector3 minBounds = spawnArea.bounds.min;
            Vector3 maxBounds = spawnArea.bounds.max;
            int enemyCount = UnityEngine.Random.Range(4, 12);
            hasSpawnedEnemies = true;
            for (int i = 0; i < enemyCount; i++)
            {
                float randomX = UnityEngine.Random.Range(minBounds.x, maxBounds.x);
                float randomZ = UnityEngine.Random.Range(minBounds.z, maxBounds.z);
                Vector3 randomPosition = new Vector3(randomX, 1f, randomZ);
                GameObject enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            }
        }
        
        currentAngle += 30f;
    }

    
    public void Shoot()
    {
        GameObject bullet = GetPooledBullet();
        if (bullet != null)
        {
            bullet.transform.position = shootingPoint.position;
            bullet.SetActive(true);

            Vector3 shootingDirection = (player.position - shootingPoint.position).normalized;
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = shootingDirection * shootingSpeed;
        }
    }

    private GameObject GetPooledBullet()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                return bulletPool[i];
            }
        }
        return null;
    }

    public void TakeDamage(float damage)
    {
        healthBar.fillAmount -= healthBar.fillAmount / Hp;
        Hp -= damage;
    }
}
