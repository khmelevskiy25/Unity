using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;

    [SerializeField]
    private float moveSpeed = 10.0f;

    [SerializeField]
    private Vector3 shift = new Vector3(0, 0, -13.56f);

    private void Update()
    {
        if (target == null)
        {
            var player = FindObjectOfType<PlayerController>();

            if (player != null)
                target = player.transform;
        }

        if (target == null)
            return;

        transform.position = Vector3.Lerp(transform.position, target.position + shift, Time.deltaTime * moveSpeed);
    }

}
