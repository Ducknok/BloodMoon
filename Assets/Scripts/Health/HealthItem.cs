using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public Health health;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(health.currentHealth >= health.maxHealth)
        //{
        //    return;
        //}
            if (collision.name == "Player")
            {
                health.playerHealth();
                Destroy(gameObject);
            }
    }
}
