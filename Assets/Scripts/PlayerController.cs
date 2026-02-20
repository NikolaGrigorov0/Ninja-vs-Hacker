using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpImpulse = 10f;

    private int brJumped = 0;
    public int maxxbrJumped = 2;

    [Header("Attack Settings")]
    public GameObject swordHitbox;
    private bool isAttack;

    Vector2 moveInput;
    TouchingDirections touchingDirections;

    Rigidbody2D rb;
    Animator animator;

    public float CurrentMoveSpeed
    {
        get
        {
            if (IsMoving && !touchingDirections.IsOnWall)
            {
                return IsRunning ? runSpeed : walkSpeed;
            }
            else
            {
                return 0;
            }
        }
    }

    private bool _isMoving = false;
    public bool IsMoving
    {
        get { return _isMoving; }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    private bool isRunning = false;
    public bool IsRunning
    {
        get { return isRunning; }
        set
        {
            isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    private bool hitFXInternal = false;

    public bool HitFX
    {
        get { return hitFXInternal; }
        set
        {
            hitFXInternal = value;
            animator.SetBool(AnimationStrings.HitFX, value);
        }
    }

    public bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;
            }
            _isFacingRight = value;
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
        {
            brJumped = 0;
        }

        if (!touchingDirections.IsOnWall)
        {
            SetFacingDirection(moveInput);
            rb.linearVelocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.linearVelocity.y);
        }
    }

    IEnumerator Attack()
    {
        animator.SetTrigger(AnimationStrings.HitFX); // стартира анимацията

        if (swordHitbox != null)
            swordHitbox.SetActive(true); // включва hitbox

        yield return new WaitForSeconds(0.3f); // продължителност на атаката

        if (swordHitbox != null)
            swordHitbox.SetActive(false); // изключва hitbox
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            HitFX = true;   // сетва анимацията на HitFx
            isAttack = true; // стартира корутината за атака
        }
    }

    public void OnAttackEnd()
    {
        if (swordHitbox != null)
            swordHitbox.SetActive(false); // изключва hitbox
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && brJumped < maxxbrJumped)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpImpulse);
            brJumped++;
        }
    }
}