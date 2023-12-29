using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;

    private Vector2 moveInput;
    private Animator anim;
    public int moveSpeed;
    public int dashValue;
    int activeSpeed;

    Vector2 moveDirection;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        activeSpeed = moveSpeed;
    }
    void Update()
    {
        Inputs();
        Animate();
    }

    void FixedUpdate()
    {
        Move();
        /*{
            Dash();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            activeSpeed = moveSpeed;
        }*/
    }

    void Inputs()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y= Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
    }
    void Move()
    {
        rb.velocity = new Vector3(moveInput.x * activeSpeed, rb.velocity.y, moveInput.y * activeSpeed);
    }
    /*void Dash()
    {
        rb.velocity = new Vector2(moveDirection.x * activeSpeed, moveDirection.y * activeSpeed)*dashValue;
    }*/

    private void Animate(){
        anim.SetFloat("MovementX", moveInput.x);
        anim.SetFloat("MovementY", moveInput.y);
        anim.SetFloat("Speed", moveInput.sqrMagnitude);
    }
}
