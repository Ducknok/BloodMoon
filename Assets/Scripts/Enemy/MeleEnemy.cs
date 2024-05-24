using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemy : MonoBehaviour
{
    [Header("Attack Paremeters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Paremeters")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    //References
    private Animator anim;
    [SerializeField] private Health playerHealth;
    private EnemyPatrol enemyPatrol;

    private void Start()
    {
        anim = GetComponent<Animator>();

        //GetComponentInParent
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        //Attack only when player in sight
        if (PlayerInsight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("attack");
            }
        }
        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInsight();
        }
    }

    //Check player trong tam danh
    private bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
           new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
           , 0, Vector2.left, 0, playerLayer);
        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    // Gay sat thuong len player
    private void DamagePlayer()
    {
        if (PlayerInsight())
        {
            //Damage player health
            playerHealth.TakeDamage(damage);
        }
    }
}
