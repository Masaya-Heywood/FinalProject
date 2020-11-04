using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float dampTime = 0.15f;
    public Transform target;

    private Vector3 velocity;

    public Vector3 initialPos;

    private void FixedUpdate()
    {
        if (!target) return;

        var camera = Camera.main;
        var selfPosition = transform.position;
        var targetPosition = target.position;
        var point = camera.WorldToViewportPoint(targetPosition);
        var delta = targetPosition - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        var destination = selfPosition + delta;
        transform.position = Vector3.SmoothDamp(selfPosition, destination + initialPos, ref velocity, dampTime);
    }
}
