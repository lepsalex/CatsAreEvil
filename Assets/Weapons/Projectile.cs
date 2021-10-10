using UnityEngine;

namespace Weapons
{
    public class Projectile : MonoBehaviour
    {
        public int damage;
        public float speed;
        public float lifetime;

        public GameObject explosion;

        private void Start()
        {
            Invoke(nameof(DestroyProjectile), lifetime);
        }

        private void Update()
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        private void DestroyProjectile()
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.Equals("Enemy"))
            {
                other.GetComponent<Enemy.Enemy>().TakeDamage(damage);
                DestroyProjectile();
            }
        }
    }
}