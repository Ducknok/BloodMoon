using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifeTime;
    private Animator anim;
    private bool hit;
    private BoxCollider2D collider2D;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        collider2D = GetComponent<BoxCollider2D>();
    }
    public void ActivateProjectile()
    {
        hit = false;
        lifeTime = 0;
        gameObject.SetActive(true);
        collider2D.enabled = true;
    }

    private void Update()
    {
        if (hit) return;
        float moveSpeed = speed * Time.deltaTime;
        transform.Translate(moveSpeed, 0, 0);

        lifeTime += Time.deltaTime;
        if(lifeTime > resetTime) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Boss") return;
        if(collision.tag == "Player")
        {
            hit = true;
            base.OnTriggerEnter2D(collision); // Execute logic from parent script first
            gameObject.SetActive(false); // when this hits any object deactivate arrow
            collider2D.enabled = false;

            if (anim != null)
                anim.SetTrigger("explode");
            else
                gameObject.SetActive(false);
        }
        
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
