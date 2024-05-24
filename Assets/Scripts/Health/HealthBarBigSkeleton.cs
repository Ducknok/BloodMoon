using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBigSkeleton : MonoBehaviour
{
    [SerializeField] private Health objectHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealth;
    [SerializeField] private Transform enemy;
    [SerializeField] private Transform healthBar;
    public Vector3 offset; // Khoảng cách từ đỉnh của enemy đến thanh health bar

    private void Start()
    {

        totalHealthBar.fillAmount = objectHealth.maxHealth / 7;
    }
    private void Update()
    {
        currentHealth.fillAmount = objectHealth.maxHealth / 7;
    }
    private void FixedUpdate()
    {
        healthBar.transform.position = Camera.main.WorldToScreenPoint(enemy.position + offset);
    }
}
