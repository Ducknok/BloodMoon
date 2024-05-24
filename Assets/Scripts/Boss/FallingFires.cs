using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFires : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D coll;
    public float distance; 
    bool isFalling = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        Physics2D.queriesStartInColliders = false;
        if(isFalling == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance);
            Debug.DrawRay(transform.position, Vector2.down * distance, Color.yellow);
            Physics2D.queriesStartInColliders = true;
            
            if(hit.transform != null)
            {
                //if (hit.transform.tag == "Boss") return;
                if (hit.transform.tag == "Player")
                {
                    rb.gravityScale = 5;
                    isFalling = true;
                }
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Boss")
        //{
        //    Debug.Log("da va vao boss");
        //    return;
        //}


        if (collision.gameObject.tag == "Ground")
        {
            rb.gravityScale = 0;
            coll.enabled = false;
        }
    }
}
