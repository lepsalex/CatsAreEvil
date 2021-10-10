using System.Collections;
using UnityEngine;
using Utils;

namespace Enemy
{
    public class MeleeEnemy : Enemy
    {
        public float attackRangeMoveSpeed;
        public float stopDistance;

        private float _movementSpeed;
        private float _attackTime;

        private RangeFinder _rangeFinder;
        private Animator _animator;

        private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

        protected override void Start()
        {
            _animator = GetComponent<Animator>();

            // set movementSpeed to speed by default
            _movementSpeed = speed;

            // setup rangefinder component
            _rangeFinder = GetComponentInChildren<RangeFinder>();
            _rangeFinder.onEnter += OnRangeEnter;
            _rangeFinder.onExit += OnRangeExit;

            base.Start();
        }

        private void Update()
        {
            if (player == null) return;

            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position =
                    Vector2.MoveTowards(transform.position, player.position, _movementSpeed * Time.deltaTime);
            }
            else
            {
                if (!(Time.time >= _attackTime)) return;

                StartCoroutine(Attack());
                _attackTime = Time.time + timeBetweenAttacks;
            }
        }

        private void OnRangeEnter(Collider2D other)
        {
            if (!other.tag.Equals("Player")) return;

            _movementSpeed = attackRangeMoveSpeed;
            _animator.SetBool(IsAttacking, true);
        }

        private void OnRangeExit(Collider2D other)
        {
            if (!other.tag.Equals("Player")) return;

            _movementSpeed = speed;
            _animator.SetBool(IsAttacking, false);
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
                var formula = (-Mathf.Pow(percent, 2) + percent) * 4;
                transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
                yield return null;
            }
        }
    }
}