using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene1Player : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
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
    }
}
