using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveSaw : MonoBehaviour
{
    [SerializeField]
    private Transform targetPosition;

    [SerializeField]
    private Rigidbody2D body;

    private Vector2 initialPosition;

    private Sequence moveSequence;

    private void Start()
    {
        initialPosition = transform.position;
        SawMove();
    }

    private void SawMove()
    {
        moveSequence = DOTween.Sequence();
        moveSequence.Append(body.DOMove(targetPosition.position, 3.0f));
        moveSequence.AppendInterval(2.0f);
        moveSequence.Append(body.DOMove(initialPosition, 3.0f));
        moveSequence.AppendInterval(2.0f);
        moveSequence.SetLoops(-1);
    }
}
