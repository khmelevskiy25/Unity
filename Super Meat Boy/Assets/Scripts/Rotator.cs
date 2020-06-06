using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float speedSaw;

    private void Update()
    {
        transform.Rotate(0, 0, -180.0f * Time.deltaTime * speedSaw);
    }
}
