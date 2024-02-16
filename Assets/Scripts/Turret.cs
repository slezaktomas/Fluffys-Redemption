using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject alert;
    [SerializeField] private int bulletPoolSize = 15;
    private List<GameObject> bulletPool = new List<GameObject>();
    [SerializeField] private float shootingSpeed;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private Transform shootingPoint;

    private void Start()
    {
        StartCoroutine(AlertEnable());
        Invoke("StartShooting", 2f);
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    void Update()
    {
    }

    float currentAngle = 0f;
    bool isShooting = false;

    private void StartShooting()
    {
        StartCoroutine(ShootCircleEveryTwoSeconds());
    }

    IEnumerator ShootCircleEveryTwoSeconds()
    {
        while (true)
        {
            if (!isShooting)
            {
                isShooting = true;
                Shoot();
                yield return new WaitForSeconds(2f);
                isShooting = false;
            }
            else
            {
                yield return null;
            }
        }
    }

    void Shoot()
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
                rb.velocity = direction.normalized * shootingSpeed;
            }
        }
        alert.SetActive(false);
    }

    IEnumerator AlertEnable()
    {
        yield return new WaitForSeconds(1f);
        alert.SetActive(true);
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
}
