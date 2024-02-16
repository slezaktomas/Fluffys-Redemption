using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    public AudioSource movementSound;

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
        movementSound = GetComponent<AudioSource>();
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
    }

    void Inputs()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
    }
    void Move()
    {
        rb.velocity = new Vector3(moveInput.x * activeSpeed, rb.velocity.y, moveInput.y * activeSpeed);

        
        if (moveInput.magnitude > 0)
        {
            if (!movementSound.isPlaying)
            {
                movementSound.Play();
            }
        }
        else
        {
            movementSound.Stop();
        }
    }

    private void Animate()
    {
        anim.SetFloat("MovementX", moveInput.x);
        anim.SetFloat("MovementY", moveInput.y);
        anim.SetFloat("Speed", moveInput.sqrMagnitude);
    }
}