using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FallingSaw : MonoBehaviour
{
    [SerializeField]
    private Transform targetPosition;

    [SerializeField]
    private Rigidbody2D body;

    [SerializeField]
    private float fallSpeed;

    [SerializeField]
    private float intervalSpeed;

    private Vector2 initialPosition;

    private Sequence moveSequence;

    private void Start()
    {
        initialPosition = transform.position;
        FallSaw();
    }

    private void FallSaw()
    {
        moveSequence = DOTween.Sequence();
        moveSequence.Append(body.DOMove(targetPosition.position, fallSpeed));
        moveSequence.Append(body.DOMove(initialPosition, 0f));
        moveSequence.AppendInterval(intervalSpeed);
        moveSequence.SetLoops(-1);
    }
}
