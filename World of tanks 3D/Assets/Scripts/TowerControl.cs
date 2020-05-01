using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    [SerializeField]
    private Transform barrel;

    private void Update()
    {
        var mainCamera = Camera.main;
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
            return;

        var difference = hit.point - transform.position;

        var rotation = Quaternion.FromToRotation(transform.forward, difference);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation * transform.rotation, 100.0f * Time.deltaTime);

        var currentRotation = transform.localEulerAngles;

        currentRotation.x = 0;
        currentRotation.z = 0;

        transform.localEulerAngles = currentRotation;

        barrel.LookAt(hit.point);

        var barrelRotation = barrel.localEulerAngles;
        barrelRotation.z = 0;
        barrelRotation.y = 0;

        barrel.localEulerAngles = barrelRotation;    
    }
}
