using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health objectHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealth;
    public GameOver gameOver;


    private void Start()
    {
        totalHealthBar.fillAmount = objectHealth.currentHealth / 10;
    }
    private void Update()
    {
        currentHealth.fillAmount = objectHealth.currentHealth / 10;
        if(objectHealth.currentHealth <= 0)
        {
            gameObject.SetActive(false);
            gameOver.EndGame();   
        }
    }
}
