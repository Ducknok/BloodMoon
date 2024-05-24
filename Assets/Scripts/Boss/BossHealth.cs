using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float health = 100f;
    public bool isInvulnerable = false;
    public Animator animator;

    public void TakeDamage(float damage)
    {
        if (isInvulnerable) return;
        health -= damage;

        
        if(health <= 0)
        {
            animator.SetTrigger("death");
        }
    }
}
