using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve powerCurve;

    [SerializeField]
    private float powerScaler = 1000.0f;

    [SerializeField]
    private float breakingPower = 1000.0f;

    [SerializeField]
    private float rotationTorque = 10000.0f;

    [SerializeField]
    private List<WheelCollider> rightWheels;

    [SerializeField]
    private List<WheelCollider> leftWheels;

    [SerializeField]
    private AudioSource engineSource;

    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        var verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");

        engineSource.pitch = 1.0f + Mathf.Abs(verticalInput) * 0.3f + Mathf.Abs(horizontalInput) * 0.15f;

        var rightWheelsInput = verticalInput;
        var leftWheelsInput = verticalInput;

        var verticalSigh = Mathf.Sign(verticalInput);
        if (verticalSigh == 0.0f)
            verticalSigh = 0.0f;

        if (horizontalInput < 0.0f)
        {
            leftWheelsInput = -0.25f * verticalSigh;
            rightWheelsInput = 1.0f * verticalSigh;
        }

        if (horizontalInput > 0.0f)
        {
            leftWheelsInput = 1.0f * verticalSigh;
            rightWheelsInput = -0.25f * verticalSigh;
        }

        UpdateWheels(leftWheels, leftWheelsInput);
        UpdateWheels(rightWheels, rightWheelsInput);
    }

    private void UpdateWheels(List<WheelCollider> wheels, float verticalInput)
    {
        foreach (var wheel in wheels)
        {
            if (verticalInput * wheel.rpm < 0)
            {
                wheel.brakeTorque = breakingPower;
                wheel.motorTorque = 0.0f;
            }
            else
            {
                wheel.motorTorque = verticalInput * powerCurve.Evaluate(Mathf.Abs(wheel.rpm)) * powerScaler;
                wheel.brakeTorque = 0.0f;
            }
        }
    }
}
