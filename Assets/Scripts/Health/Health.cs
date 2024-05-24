using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float startingHeath;
    public float maxHealth { get;  set; }
    public float currentHealth;
    private Animator anim;
    public bool dead;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    AudioManager manager;
    

    private void Awake()
    {
        maxHealth = startingHeath;
        currentHealth = startingHeath;
        anim = GetComponent<Animator>();
        manager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }
    public void TakeDamage(float _damage)
    {
        maxHealth = Mathf.Clamp(maxHealth - _damage, 0, startingHeath);
        currentHealth -= _damage;
        
        if (maxHealth > 0)
        {
            //player hurt
            anim.SetTrigger("hittake");
            manager.PlaySFX(manager.hittake);
            //iframes
        }
        else
        {
            if (!dead)
            {

                anim.SetTrigger("death");

                //Deactivate all attached components
                foreach (Behaviour comp in components)
                {
                    comp.enabled = false;
                }
                dead = true;
                manager.PlaySFX(manager.death);

            }
        }

        
    }
    public void playerHealth()
    {
        currentHealth += 2;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            
        }
        
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
