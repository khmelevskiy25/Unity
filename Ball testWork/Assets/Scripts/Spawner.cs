using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;

    private GameObject currentBall;

    public GameObject CurrentBall
    {
        get
        {
            return currentBall;
        }
    }

    private void Start()
    {
        Create();

        Events.OnCleared += OnCleared;
    }

    private void OnDestroy()
    {
        Events.OnCleared -= OnCleared;
    }

    private void OnCleared()
    {
        if (currentBall != null)
            Destroy(currentBall);

        Create();
    }

    private void Create()
    {
        currentBall = Instantiate(ball, transform.position, transform.rotation);
    }

    private void Update()
    {
        if (currentBall != null)
            return;

        Create();
    }
}
