using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public float speed;

    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private Vector2 moveAmount;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;

        if (moveAmount != Vector2.zero)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        rigidbody2d.MovePosition(rigidbody2d.position + moveAmount * Time.fixedDeltaTime);
    }
    
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0) Destroy(gameObject);
    }
}