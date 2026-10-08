using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingPlatform : MonoBehaviour
{
    public float timeBeforeDrop;
    public Component[] childComponents;

    Rigidbody2D rb;
    BoxCollider2D collider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = (BoxCollider2D)childComponents[1];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Invoke(nameof(Drop), timeBeforeDrop);
        }
    }

    private void Drop()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    // Start is called before the first frame update
    void Start()
    {
        childComponents = GetComponentsInChildren<BoxCollider2D>();
    }
}
