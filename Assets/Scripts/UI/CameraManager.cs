using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float dampTime = 0.15f;
    public Transform target;

    private Vector3 velocity;

    public Vector3 initialPos;

    private PlayerController player;

    //how much the mouse movement affects the Camera
    public float moveByMouse = 0.1f;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (!target) return;

        var camera = Camera.main;
        var selfPosition = transform.position;
        var targetPosition = target.position;
        var point = camera.WorldToViewportPoint(targetPosition);
        var delta = targetPosition - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        var destination = selfPosition + delta;
        var mouseAng = new Vector3(Mathf.Cos(Mathf.Deg2Rad*player.getMouseAngle()), Mathf.Sin(Mathf.Deg2Rad * player.getMouseAngle()),0);
        mouseAng *= moveByMouse;
        transform.position = Vector3.SmoothDamp(selfPosition + mouseAng, destination + initialPos + mouseAng, ref velocity, dampTime);
    }
}
