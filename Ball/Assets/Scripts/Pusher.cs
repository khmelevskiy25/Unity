using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    [SerializeField]
    private float power = 100.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInput>() == null)
            return;

        other.attachedRigidbody.AddForce(transform.forward * power, ForceMode.Impulse);
    }
}
