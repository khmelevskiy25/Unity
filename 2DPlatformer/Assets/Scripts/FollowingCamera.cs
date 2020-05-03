using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float moveSpeed = 10.0f;

    [SerializeField]
    private Vector3 shift = new Vector3(0, 0, 10);
    
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + shift, Time.deltaTime * moveSpeed);
    }
}
