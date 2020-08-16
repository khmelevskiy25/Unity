using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Spawner spawner;

    [SerializeField]
    private Rigidbody body;

    [SerializeField]
    private float initialSpeed = 0.5f;

    private float speed;

    private Vector3 initialPosition;

    private void Start()
    {
        speed = initialSpeed;
        initialPosition = transform.position;

        Events.OnCleared += OnCleared;
    }

    private void OnDestroy()
    {
        Events.OnCleared -= OnCleared;
    }

    private void OnCleared()
    {
        transform.position = initialPosition;
        speed *= 2.0f;
    }

    private void FixedUpdate()
    {
        if (spawner.CurrentBall == null)
            return;

        var currentPosition = transform.position;
        var currentBallPosition = spawner.CurrentBall.transform.position;

        currentPosition.z = currentBallPosition.z;

        transform.position = Vector3.MoveTowards(transform.position, currentPosition, Time.deltaTime * speed);
    }
}
