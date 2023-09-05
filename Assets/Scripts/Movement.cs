using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public int moveSpeed;
    public int dashValue;
    int activeSpeed;

    Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        activeSpeed = moveSpeed;
    }
    void Update()
    {
        Inputs();
    }

    void FixedUpdate()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            activeSpeed = moveSpeed;
        }
    }

    void Inputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }
    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * activeSpeed, moveDirection.y * activeSpeed);
    }
    void Dash()
    {
        rb.velocity = new Vector2(moveDirection.x * activeSpeed, moveDirection.y * activeSpeed)*dashValue;
    }
}
