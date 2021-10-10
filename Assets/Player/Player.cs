using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public float speed;

    private Rigidbody2D _rigidbody2d;
    private Animator _animator;
    private Vector2 _moveAmount;

    private static readonly int IsRunning = Animator.StringToHash("isRunning");

    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _moveAmount = moveInput.normalized * speed;

        if (_moveAmount != Vector2.zero)
        {
            _animator.SetBool(IsRunning, true);
        }
        else
        {
            _animator.SetBool(IsRunning, false);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2d.MovePosition(_rigidbody2d.position + _moveAmount * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0) Destroy(gameObject);
    }
}