using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBoss : MonoBehaviour
{
    [SerializeField] private Health objectHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealth;


    private void Start()
    {
        totalHealthBar.fillAmount = objectHealth.maxHealth / 100;
    }
    private void Update()
    {
        currentHealth.fillAmount = objectHealth.maxHealth / 100;
    }
}
