using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    [SerializeField]
    private TankControl tank;

    [SerializeField]
    private Text speedText;

    private void Update()
    {
        var body = tank.GetComponent<Rigidbody>();

        var velocity = Vector3.Dot(body.transform.forward, body.velocity);
        velocity *= 60.0f;
        velocity *= 60.0f;
        velocity /= 1000.0f;
        velocity = Mathf.Abs(velocity);

        speedText.text = velocity.ToString("0.00");
    }
}
