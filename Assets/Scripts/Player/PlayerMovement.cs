using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private BoxCollider2D coll;
    [SerializeField] private Transform player;
    [SerializeField] private TrailRenderer tr;
    private EnemySideways enemySideway;

    
    [Header("Layer Mask")]
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask jumpableWooden;

    [Header("Run")]
    // run

    [SerializeField] private float dirX;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isFacingRight;

    
    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpStartTime;
    [SerializeField] private float jumpTime;
    [SerializeField] private bool isJumping;
    [SerializeField] private int maxJumps;
    [SerializeField] private int availableJumps;

    [Header("Dash")]
    [SerializeField] private bool isDashing;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float distanceBetweenImages;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashTimeLeft;
    [SerializeField] private float lastImageXpos;
    [SerializeField] private float lastDash = -100f;
    [SerializeField] private float facingDirection = 1f;
    [SerializeField] private Collider2D[] collider2Ds;

    [Header("Audio")]
    AudioManager audioManager;

    private enum MovemenState { idle, run, jump, fall }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        player = GetComponent<Transform>();
        tr = GetComponent<TrailRenderer>();
        moveSpeed = 10f;
        isFacingRight = true;
        jumpForce = 8f;
        jumpStartTime = 0.25f;
        maxJumps = 2;
        //wallSlidingSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        Walk();
        Jump();
        DoubleJump();
        Dash();
        CheckDash();
        UpdateAnimationState();
    }
    private void FixedUpdate()
    {
        
    }

    // ---- Walk ----
    private void Walk()
    {
        //transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }

    // ---- Xu ly va cham giua player vs mat dat, tuong, check dash
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    private bool IsWooden()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableWooden);
    }
    private void CheckDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {

                rb.velocity = new Vector2(dashSpeed * facingDirection * dirX, rb.velocity.y);
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }
        }
        
    }
    private void Jump()
    {
        if ((IsGrounded() || IsWooden()) && Input.GetButtonDown("Jump"))
        { 
            isJumping = true;
            audioManager.PlaySFX(audioManager.jump);
            jumpTime = jumpStartTime;
            //rb.velocity = Vector2.up * jumpForce;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetButton("Jump") && isJumping == true)
        {
            if (jumpTime > 0)
            {
                //rb.velocity = Vector2.up * jumpForce;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTime -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }
    private void DoubleJump()
    {
        if ((IsGrounded() || IsWooden())  && isJumping == false )
        {
            availableJumps = maxJumps;
        }
        if (Input.GetButtonDown("Jump") && availableJumps > 0)
        {
            isJumping = true;
            availableJumps--;
            audioManager.PlaySFX(audioManager.jump);
            //rb.velocity = Vector2.up * jumpForce;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }


    // ---- Xu ly luot ----
    private void Dash()
    {
        if (Input.GetButtonDown("Dash"))
        {
            
            if (Time.time >= (lastDash + dashCooldown))
            {

                AttempToDash();
            }
        }
    }
    private void AttempToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }
    private void UpdateAnimationState()
    {
        MovemenState state;
        if (dirX > 0f)
        {
            state = MovemenState.run;
            Flip();
        }
        else if (dirX < 0f)
        {
            state = MovemenState.run;
            Flip();
        }
        else state = MovemenState.idle;

        if (rb.velocity.y > .1f)
        {

            state = MovemenState.jump;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovemenState.fall;

        }
        anim.SetInteger("state", (int)state);
    }

        // ---- Xu ly quay mat sang trai và phai ----
        private void Flip()
    {
        if (isFacingRight && dirX < 0f || !isFacingRight && dirX > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


}
