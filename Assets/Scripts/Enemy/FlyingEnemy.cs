using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [Header("Enemy Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform startingPoint;
    [SerializeField] public bool chase = false;
    [SerializeField] private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Player == null)
        {
            return;
        }
        if (chase)
        {
            Chase();
        }
        else
        {
            ReturnStartPoint();
        }
        Flip();
    }
    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, Player.transform.position) <= 0.5f)
        {
            //shoot, animation
        }
    }
    private void ReturnStartPoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
    }
    private void Flip()
    {
        if(transform.position.x > Player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
