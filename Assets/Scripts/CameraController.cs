﻿using UnityEngine;
using System.Collections;
using Assets.Scripts.Helpers;

public class CameraController : MonoBehaviour
{

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    private Transform target;

    public float LevelWidth;
    public float LevelHeight;

    public void Update()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        if (target)
        {
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            if (destination.x < -LevelWidth / 2)
                destination.x = -LevelWidth / 2;

            if (destination.x > LevelWidth / 2)
                destination.x = LevelWidth / 2;

            if (destination.y < -LevelHeight / 2)
                destination.y = -LevelHeight / 2;

            if (destination.y > LevelHeight / 2)
                destination.y = LevelHeight / 2; 
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

    }
}