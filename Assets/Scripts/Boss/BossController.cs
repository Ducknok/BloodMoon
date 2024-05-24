using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    Transform player;
    Rigidbody2D rb;
    Boss boss;
    Animator anim;

    public float attackRange = 3f;
    public float speed = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        boss = GetComponent<Boss>();
        anim = GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {
        boss.LookAtPlayer();


        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            anim.SetTrigger("attack");
        }
    }
}
