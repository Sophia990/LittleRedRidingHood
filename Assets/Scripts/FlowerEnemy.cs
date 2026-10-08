using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerEnemy : MonoBehaviour
{
    public float speed = 3f;
    public DetectionZone attackZone;
    public DetectionZoneMove chaseZone;

    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;
    Damageable damageable;

    public enum WalkableDirection { Right, Left }

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                // Direction flipped
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }

            _walkDirection = value;
        }
    }

    public bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool _hasChaseTarget = false;
    public bool HasChaseTarget
    {
        get
        {
            return _hasChaseTarget;
        }
        private set
        {
            _hasChaseTarget = value;
            animator.SetBool(AnimationStrings.hasChaseTarget, value);
        }
    }

    public bool CanMove => animator.GetBool(AnimationStrings.canMove);

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        private set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        HasChaseTarget = chaseZone.detectedColliders.Count > 0;

        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (HasChaseTarget)
        {
            if (!damageable.IsHit)
            {
                if (CanMove && touchingDirections.IsGrounded)
                {
                    Transform target = chaseZone.detectedColliders[0].transform;
                    if (target.transform.position.x - transform.position.x > 0) // player right of flower
                    {
                        if (WalkDirection == WalkableDirection.Left)
                        {
                            FlipDirection();
                        }
                    }
                    else if (target.transform.position.x - transform.position.x < 0) // player left of flower
                    {
                        if (WalkDirection == WalkableDirection.Right)
                        {
                            FlipDirection();
                        }
                    }
                    rb.velocity = new Vector2(speed * walkDirectionVector.x, rb.velocity.y);
                }
            }
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
