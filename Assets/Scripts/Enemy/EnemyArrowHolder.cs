using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrowHolder : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    private void Start()
    {
        enemy = GetComponent<Transform>();
    }

    private void Update()
    {
        transform.localScale = enemy.localScale;
    }
}
