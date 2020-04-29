using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private BirdSpawner spawner;

    private Vector3 initialPosition;

    private float shift = 0.0f;

    [SerializeField] private float maxChange = 2.0f;

    private void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        var change = speed * Time.deltaTime * Mathf.Max(spawner.AliveTime, 1.0f);
        shift += change;
        transform.position += Vector3.left * change;
        if (shift > maxChange)
        {
            shift = 0.0f;
            transform.position = initialPosition;
        }
    }
}
