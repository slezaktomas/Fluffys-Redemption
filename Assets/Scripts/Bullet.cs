using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            gameObject.SetActive(false);
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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BulletZone"))
        {
            gameObject.SetActive(false);
        }
    }
}
