using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpImpulse = 10f;
    public int maxJumpCount = 2;

    [Header("Attack Settings")]
    public GameObject swordHitbox;
    public float attackDuration = 0.3f; // колко дълго swordHitbox е активен

    private int jumpCount = 0;
    private bool isAttack = false;

    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    TouchingDirections touchingDirections;

    private bool _isFacingRight = true;
    private bool _isMoving = false;
    private bool isRunning = false;

    public float CurrentMoveSpeed => (_isMoving && !touchingDirections.IsOnWall) ? (isRunning ? runSpeed : walkSpeed) : 0;

    public bool IsMoving
    {
        get => _isMoving;
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    public bool IsRunning
    {
        get => isRunning;
        set
        {
            isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();

        if (swordHitbox != null)
            swordHitbox.SetActive(false);
    }

    private void Update()
    {
        animator.SetFloat(AnimationStrings.yVelocity, rb.linearVelocity.y);

        if (isAttack)
        {
            StartCoroutine(Attack());
            isAttack = false;
        }
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded)
            jumpCount = 0;

        if (!touchingDirections.IsOnWall)
        {
            SetFacingDirection(moveInput);
            rb.linearVelocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.linearVelocity.y);
        }
    }

    IEnumerator Attack()
    {
        // стартираме Trigger в Animator
        animator.SetTrigger(AnimationStrings.HitFX);

        // включваме swordHitbox
        if (swordHitbox != null)
            swordHitbox.SetActive(true);

        // изчакваме продължителността на атаката
        yield return new WaitForSeconds(attackDuration);

        // изключваме swordHitbox
        if (swordHitbox != null)
            swordHitbox.SetActive(false);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isAttack = true; // стартира корутината
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
            IsRunning = true;
        else if (context.canceled)
            IsRunning = false;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && jumpCount < maxJumpCount)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpImpulse);
            animator.SetTrigger(AnimationStrings.jump);
            jumpCount++;
        }
    }

    private void SetFacingDirection(Vector2 input)
    {
        if (input.x > 0 && !IsFacingRight)
            IsFacingRight = true;
        else if (input.x < 0 && IsFacingRight)
            IsFacingRight = false;
    }
}