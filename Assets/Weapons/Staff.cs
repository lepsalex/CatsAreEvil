using System;
using UnityEngine;
using Utils;
using Debug = System.Diagnostics.Debug;

namespace Weapons
{
    public class Staff : MonoBehaviour
    {
        public GameObject projectile;
        public Transform shotPoint;
        public float timeBetweenShots;

        private float _shotTIme;

        private void Update()
        {
            transform.rotation = GetRotationFromTransformPosition(transform.position);

            // On Left-Mouse button press and if enough time since last shot
            if (Input.GetMouseButton(0) && Time.time >= _shotTIme)
            {
                ShootProjectile();
            }
        }

        private void ShootProjectile()
        {
            Instantiate(projectile, shotPoint.position, transform.rotation);
            _shotTIme = Time.time + timeBetweenShots;
        }

        private static readonly Func<Vector3, Vector2> GetMouseDirection =
            position =>
            {
                Debug.Assert(Camera.main != null, "Camera.main != null");
                return Camera.main.ScreenToWorldPoint(Input.mousePosition) - position;
            };

        private static readonly Func<Vector2, float> GetDirectionDegrees =
            direction => Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        private static readonly Func<float, Quaternion> GetRotation = angle =>
            Quaternion.AngleAxis(angle - 90, Vector3.forward);

        private static readonly Func<Vector3, Quaternion> GetRotationFromTransformPosition =
            GetMouseDirection.AndThen(GetDirectionDegrees).AndThen(GetRotation);
    }
}