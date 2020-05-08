using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera frontCamera;

    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera backCamera;

    [SerializeField]
    private float forceAmount = 100.0f;

    [SerializeField]
    private float jumpPower = 100.0f;

    [SerializeField]
    private float raycastLength = 1.1f;

    [SerializeField]
    private int liveCount = 3;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip getCoin;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private float CameraSpeed = 10.0f;

    [SerializeField]
    private float timer = 30f;

    private int score = 0;

    public float SpeedBonus;

    private CheckPoint currentCheckpoint;

    private Rigidbody body;

    private Vector3 initalPosition;

    private Vector3 startPosition;

    private List<int> scores = new List<int>();

    private void Start()
    {
        backCamera.gameObject.SetActive(false);
        Application.targetFrameRate = 60;
        initalPosition = transform.position;
        startPosition = transform.position;
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        var hInput = Input.GetAxis("Horizontal");
        var vInput = Input.GetAxis("Vertical");

        var camera = Camera.main;

        var right = camera.transform.right;
        right.y = 0;
        right.Normalize();

        var forward = camera.transform.forward;
        forward.y = 0;
        forward.Normalize();

        var totalForce = forceAmount + SpeedBonus;

        body.AddForce(right * totalForce * hInput);
        body.AddForce(forward * totalForce * vInput);

        if (Input.GetButtonDown("Jump") && Physics.Raycast(new Ray(transform.position, Vector3.down), raycastLength))
            body.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            backCamera.gameObject.SetActive(!backCamera.gameObject.activeSelf);
            frontCamera.gameObject.SetActive(!frontCamera.gameObject.activeSelf);
        }

        if (timer <= 0)
        {
            transform.position = startPosition;
            timer = 30.0f;
        }
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Coin>())
        {
            score++;
            audioSource.PlayOneShot(getCoin);
        }

        if (other.GetComponent<Restarter>())
        {
            liveCount--;
            if (liveCount <= 0)
            {
                currentCheckpoint = null;
                initalPosition = startPosition;
                liveCount = 3;

                scores.Add(score);
                scores.Sort((x, y) => y.CompareTo(x));
                var currentScore = scores.GetRange(0, Mathf.Min(scores.Count, 10));

                string totalScore = string.Empty;
                foreach (var scoreToAdd in scores)
                    totalScore += "\n" + scoreToAdd;

                Debug.Log(totalScore);

                score = 0;
            }

            transform.position = initalPosition;
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;

        }

        var checkPoint = other.GetComponent<CheckPoint>();
        if (checkPoint != null && checkPoint != currentCheckpoint)
        {
            currentCheckpoint = checkPoint;
            initalPosition = other.transform.position;
            liveCount = 3;
            timer += 30f;
        }

        if (other.GetComponent<Coin>() == null)
            return;
    }

}
