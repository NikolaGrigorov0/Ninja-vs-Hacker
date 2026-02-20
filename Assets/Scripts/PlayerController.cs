using System;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpImpulse = 10f;
    public int maxJumps = 2;

    private int jumpsDone = 0;
    private Vector2 moveInput;
    private TouchingDirections touchingDirections;

    private bool _isMoving = false;
    public bool IsMoving
    {
        get => _isMoving;
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    private bool isRunning = false;
    public bool IsRunning
    {
        get => isRunning;
        set
        {
            isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    private bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get => _isFacingRight;
        private set
        {
            if (_isFacingRight != value)
            {
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
                _isFacingRight = value;
            }
        }
    }

    public float CurrentMoveSpeed => (IsMoving && !touchingDirections.IsOnWall) ? (IsRunning ? runSpeed : walkSpeed) : 0f;

    [Header("Attack")]
    public float attackRange = 1f;
    public int attackDamage = 1;
    public Transform attackPoint;
    public LayerMask attackLayer;
    public float comboResetTime = 4f;

    private int attackStep = 0; // 0 = няма, 1 = атака1, 2 = атака2
    private float attackTimer = 0f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isAttacking = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    private void Update()
    {
        // Таймер за рестарт на combo
        if (attackStep != 0)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                // Preview анимация на атака 2, ако последно е била атака1
                if (attackStep == 1)
                {
                    animator.SetTrigger(AnimationStrings.attack2); // само визуално
                }
                attackStep = 0; // рестарт
            }
        }
    }

    private void FixedUpdate()
    {
        animator.SetFloat(AnimationStrings.yVelocity, rb.linearVelocity.y);

        if (touchingDirections.IsGrounded)
            jumpsDone = 0;

        if (!touchingDirections.IsOnWall)
        {
            SetFacingDirection(moveInput);
            if (!isAttacking)
                rb.linearVelocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.linearVelocity.y);
        }
    }

    // -------------------- INPUT --------------------
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started) IsRunning = true;
        else if (context.canceled) IsRunning = false;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && jumpsDone < maxJumps)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpImpulse);
            jumpsDone++;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Attack();
        }
    }

    // -------------------- ATTACK --------------------
    private void Attack()
    {
        isAttacking = true;

        // Редуване на анимации
        if (attackStep == 0 || attackStep == 2)
        {
            animator.SetTrigger(AnimationStrings.attack1);
            attackStep = 1;
        }
        else if (attackStep == 1)
        {
            animator.SetTrigger(AnimationStrings.attack2);
            attackStep = 2;
        }

        attackTimer = comboResetTime;

        // Hit detection
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayer);
        foreach (Collider2D enemy in hits)
        {
            Damageable dmg = enemy.GetComponent<Damageable>();
            if (dmg != null)
                dmg.TakeDamage(attackDamage);
        }

        isAttacking = false;
    }

    private void SetFacingDirection(Vector2 input)
    {
        if (input.x > 0 && !IsFacingRight) IsFacingRight = true;
        else if (input.x < 0 && IsFacingRight) IsFacingRight = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}