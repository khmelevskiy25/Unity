using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lift : MonoBehaviour
{
    [SerializeField]
    private Transform targetPosition;

    private Vector3 initialPosition;

    private bool isAtTop = false;

    private void Start()
    {
        initialPosition = transform.position;
    }

    public void Move()
    {
        
        transform.DOKill(false);
        transform.DOMove(isAtTop ? initialPosition : targetPosition.position, 6.0f);
        isAtTop = !isAtTop;
    }

}
