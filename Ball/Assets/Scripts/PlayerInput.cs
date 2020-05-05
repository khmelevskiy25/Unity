using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private float forceAmount = 100.0f;

    [SerializeField]
    private float jumpPower = 100.0f;

    [SerializeField]
    private float raycastLength = 1.1f;

    [SerializeField]
    private int liveCount = 3;

    [SerializeField]
    private int coin;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip getCoin;

    public float SpeedBonus;

    private CheckPoint currentCheckpoint;

    private Rigidbody body;

    private Vector3 initalPosition;

    private Vector3 startPosition;

    private void Start()
    {
        initalPosition = transform.position;
        startPosition = transform.position;
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Restarter>())
        {
            var positionLift = GetComponent<Lift>();

            positionLift.DOKill(false);

            liveCount--;
            if (liveCount <= 0)
            {
                currentCheckpoint = null;
                initalPosition = startPosition;
                liveCount = 3;
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
        }

        if (other.GetComponent<Coin>() == null)
            return;

        audioSource.PlayOneShot(getCoin);
        coin++;
        Debug.Log(coin);

    }
}
