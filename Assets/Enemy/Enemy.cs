using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        public int health;
        public float speed;
        public int damage;

        public float attackSpeed;
        public float timeBetweenAttacks;
    
        [HideInInspector]
        public Transform player;

        protected virtual void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public void TakeDamage(int damageAmount)
        {
            health -= damageAmount;

            if (health <= 0) Destroy(gameObject);
        }
    }
}