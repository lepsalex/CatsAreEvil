using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float attackRangeMoveSpeed;
    public float stopDistance;

    private float movementSpeed;
    private float attackTime;

    private RangeFinder rangeFinder;
    private Animator animator;

    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    protected override void Start()
    {
        animator = GetComponent<Animator>();

        // set movementSpeed to speed by default
        movementSpeed = speed;

        // setup rangefinder component
        rangeFinder = GetComponentInChildren<RangeFinder>();
        rangeFinder.onEnter += OnRangeEnter;
        rangeFinder.onExit += OnRangeExit;

        base.Start();
    }

    private void Update()
    {
        if (player == null) return;

        if (Vector2.Distance(transform.position, player.position) > stopDistance)
        {
            transform.position =
                Vector2.MoveTowards(transform.position, player.position, movementSpeed * Time.deltaTime);
        }
        else
        {
            if (Time.time >= attackTime)
            {
                StartCoroutine(Attack());
                attackTime = Time.time + timeBetweenAttacks;
            }
        }
    }

    private void OnRangeEnter(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            movementSpeed = attackRangeMoveSpeed;
            animator.SetBool(IsAttacking, true);
        }
    }

    private void OnRangeExit(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            movementSpeed = speed;
            animator.SetBool(IsAttacking, false);
        }
    }

    private IEnumerator Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0;

        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }
}