using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class SkeletonAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 2f;
    public float attackRange = 2f;
    public float detectionRange = 10f;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private Transform player;
    private bool isFacingRight = true;
    private bool isGrounded;
    private bool isAttacking;

    private const string ANIM_WALK = "Walk";
    private const string ANIM_ATTACK = "Attack";
    private const string ANIM_DEATH = "Death";

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        if (groundCheck == null)
        {
            GameObject gc = new GameObject("GroundCheck");
            gc.transform.SetParent(transform);
            gc.transform.localPosition = new Vector3(0, -1.5f, 0);
            groundCheck = gc.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        CheckGround();

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            Idle();
        }
    }

    void CheckGround()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
    }

    void MoveTowardsPlayer()
    {
        if (isAttacking) return;

        animator.SetBool(ANIM_WALK, true);
        animator.SetBool(ANIM_ATTACK, false);

        float direction = Mathf.Sign(player.position.x - transform.position.x);

        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(direction * walkSpeed, rb.linearVelocity.y);
        }

        if ((direction > 0 && !isFacingRight) || (direction < 0 && isFacingRight))
        {
            Flip();
        }
    }

    void Attack()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        
        animator.SetBool(ANIM_WALK, false);
        animator.SetBool(ANIM_ATTACK, true);

        float direction = Mathf.Sign(player.position.x - transform.position.x);
        if ((direction > 0 && !isFacingRight) || (direction < 0 && isFacingRight))
        {
            Flip();
        }
    }

    void Idle()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        animator.SetBool(ANIM_WALK, false);
        animator.SetBool(ANIM_ATTACK, false);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Die()
    {
        enabled = false;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        
        animator.SetBool(ANIM_WALK, false);
        animator.SetBool(ANIM_ATTACK, false);
        animator.SetTrigger(ANIM_DEATH);
        
        GetComponent<Collider2D>().enabled = false;
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        }
    }
}
