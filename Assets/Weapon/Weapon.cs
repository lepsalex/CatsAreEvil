using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

  public GameObject projectile;
  public Transform shotPoint;
  public float timeBetweenShots;

  private float shotTIme;

  private void Update() {
    transform.rotation = GetRotationFromTransformPosition(transform.position);

    // On Left-Mouse button press and if enough time since last shot
    if (Input.GetMouseButton(0) && Time.time >= shotTIme) {
      ShootProjectile();
    }
  }

  private void ShootProjectile() {
    Instantiate(projectile, shotPoint.position, transform.rotation);
    shotTIme = Time.time + timeBetweenShots;
  }

  private static readonly Func<Vector3, Vector2> GetMouseDirection = position => Camera.main.ScreenToWorldPoint(Input.mousePosition) - position;

  private static readonly Func<Vector2, float> GetDirectionDegrees = direction => Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

  private static readonly Func<float, Quaternion> GetRotation = angle => Quaternion.AngleAxis(angle - 90, Vector3.forward);

  private static readonly Func<Vector3, Quaternion> GetRotationFromTransformPosition = GetMouseDirection.AndThen(GetDirectionDegrees).AndThen(GetRotation);
}
