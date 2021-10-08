using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float speed;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    
    private void Start()
    {
        transform.position = playerTransform.position;
    }

    private void Update()
    {
        if (playerTransform == null) return;
        
        var playerPosition = playerTransform.position;
        var clampedX = Mathf.Clamp(playerPosition.x, minX, maxX);
        var clampedY = Mathf.Clamp(playerPosition.y, minY, maxY);
        transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX, clampedY), speed);
    }
}