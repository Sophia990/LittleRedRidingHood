using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMover : MonoBehaviour
{
    public Transform snake;
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 1.5f;

    int direction = 1;

    Animator animator;
    TouchingDirections touchingDirections;
    Rigidbody2D rb;
    Damageable damageable;
    SpriteRenderer snakeSprite;

    private void Awake()
    {
        touchingDirections = GetComponentInChildren<TouchingDirections>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody2D>();
        damageable = GetComponentInChildren<Damageable>();
        snakeSprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (animator.GetBool(AnimationStrings.canMove) && touchingDirections.IsGrounded && !damageable.IsHit)
        {
            Vector2 target = CurrentMovementTarget();
            snake.position = Vector2.MoveTowards(snake.position, target, speed * Time.deltaTime);
            float distance = (target - (Vector2)snake.position).magnitude;
            if (distance <= 0.1f)
            {
                direction *= -1;
                if (snakeSprite.flipX == true)
                {
                    snakeSprite.flipX = false;
                }
                else
                {
                    snakeSprite.flipX = true;
                }
            }
        }

    }

    Vector2 CurrentMovementTarget()
    {
        if (direction == 1)
        {
            return startPoint.position;
        }
        else
        {
            return endPoint.position;
        }
    }

    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;

        if (transform.localScale.x > 0)
        {
            // facing the right
            if (rb.velocity.x < 0)
            {
                // flip
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else
        {
            // facing the left
            if (rb.velocity.x > 0)
            {
                // flip
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
    }


}
