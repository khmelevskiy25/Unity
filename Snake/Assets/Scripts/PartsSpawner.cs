using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private float zoneWidth = 15.0f;

    [SerializeField]
    private float zoneLength = 15.0f;

    private GameObject currentTarget;

    public void Update()
    {
        if (currentTarget != null)
            return;

        currentTarget = Instantiate(target);

        currentTarget.transform.position = transform.position +
            new Vector3(Random.Range(-1.0f, 1.0f) * zoneWidth, 0,
            Random.Range(-1.0f, 1.0f) * zoneLength);
    }
}
