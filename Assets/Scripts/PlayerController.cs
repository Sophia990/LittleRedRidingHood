using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float airSpeed = 3f;
    public float jumpImpulse = 10f;
    public float minY = -16f;
    Vector2 moveInput;
    TouchingDirections touchingDirections;
    Damageable damageable;
    SpriteRenderer spriteRenderer;

    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        return walkSpeed;
                    }
                    else
                    {
                        return airSpeed;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0; // movement locked
            }
        }
    }


    private bool _isMoving = false;
    public bool IsMoving { get
        {
            return _isMoving;
        } private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    public bool _isFacingRight = true;

    public bool IsFacingRight { get { return _isFacingRight; } private set {
            if (_isFacingRight != value)
            {
                // flip the local scale to make the player face the opposite direction
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }

    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!damageable.IsHit)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        }
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

        if (transform.position.y < minY)
        {
            damageable.Hit(damageable.maxHealth, Vector2.zero);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            // face right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            // face left
            IsFacingRight = false;
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    // coroutines for cutscenes

    public void CutsceneOnePartOne()
    {
        StartCoroutine(CoCutsceneOnePartOne());
    }

    public IEnumerator CoCutsceneOnePartOne()
    {
        transform.position = new Vector3(-4, -2, 0);
        Vector2.MoveTowards(transform.position, new Vector2(5, -2), 1);
        spriteRenderer.flipX = true;
        Vector2.MoveTowards(transform.position, new Vector2(-4, -2), 1);
        spriteRenderer.flipX = false;
        Vector2.MoveTowards(transform.position, new Vector2(5, -2), 1);
        spriteRenderer.flipX = true;
        Vector2.MoveTowards(transform.position, new Vector2(-4, -2), 1);
        spriteRenderer.flipX = false;
        Vector2.MoveTowards(transform.position, new Vector2(5, -2), 1);
        spriteRenderer.flipX = true;
        // mum comes out here
        Vector2.MoveTowards(transform.position, new Vector2(-4, -2), 1);
        spriteRenderer.flipX = false;
        yield return new WaitForSeconds(1f);
    }
}
