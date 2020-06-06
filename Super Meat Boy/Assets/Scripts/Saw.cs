using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0, 0, -180.0f * Time.deltaTime * 15);
    }
}
