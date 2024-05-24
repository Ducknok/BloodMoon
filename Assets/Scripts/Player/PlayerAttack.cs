using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private Animator anim;
    [SerializeField] private float attack;
    [SerializeField] private float attackNumber;
    [SerializeField] private bool isAttacking;
    [SerializeField] private float attackTemp;
    [SerializeField] private float attackTiming;
    [SerializeField] private float range;
    [SerializeField] private float damage;

    [Header("Collider Parameter")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;

    [Header("Player Parameter")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Health enemyHealth;

    AudioManager audioManager;


    void Start()
    {
        anim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        isAttacking = false;
        attack = 1f;
        attackTiming = 0.5f;
        attackNumber = 3f;
        attackTemp = attackTiming;

        
    }

    // Update is called once per frame
    void Update()
    {
        Attack();

    }
    private void Attack()
    {
        //Giam combo temp theo thoi gian khung hinh time.deltetime
        attackTemp -= Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.J) && attackTemp < 0)
        {
            isAttacking = true;
            anim.SetTrigger("attack" + attack);
            audioManager.PlaySFX(audioManager.attack);
            attackTemp = attackTiming;
        }
        else if (Input.GetKeyUp(KeyCode.J) && attackTemp > 0 && attackTemp < 0.3f)
        {
            isAttacking = true;
            attack++;
            if(attack > attackNumber)
            {
                attack = 1f;
            }
            // trigger thuc hien animation 1 lan r thoi
            anim.SetTrigger("attack" + attack);
            audioManager.PlaySFX(audioManager.attack);
            attackTemp = attackTiming;
        }
        else if (attackTemp < 0 && !Input.GetKeyUp(KeyCode.J))
        {
            isAttacking = false;
        }
        if(attackTemp < 0)
        {
            attack = 1f;
        }

    }
    private bool EnemyInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
           new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
           , 0, Vector2.left, 0, enemyLayer);
        if (hit.collider != null)
        {
            enemyHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamageEnemy()
    {
        if(EnemyInsight())
            enemyHealth.TakeDamage(damage);
    }
}
