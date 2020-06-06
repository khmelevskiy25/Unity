using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour
{
    [SerializeField]
    private GameObject fan;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3 axis = Vector3.forward;

    private void Update()
    {
        RotateFan();
    }

    private void RotateFan()
    {
        fan.transform.Rotate(axis * Time.deltaTime * speed);
    }
}
