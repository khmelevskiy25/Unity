using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TogglePlatform : MonoBehaviour
{

    [SerializeField]
    private Transform targetPosition;

    [SerializeField]
    private Rigidbody2D body;

    [SerializeField]
    private float speedPlatform;

    private Vector2 initialPosition;

    private Sequence moveSequence;

    private void Start()
    {
        initialPosition = transform.position;
        MovePlatform();
    }

    private void MovePlatform()
    {
        moveSequence = DOTween.Sequence();
        moveSequence.Append(body.DOMove(targetPosition.position, speedPlatform));
        moveSequence.AppendInterval(2.0f);
        moveSequence.Append(body.DOMove(initialPosition, speedPlatform));
        moveSequence.AppendInterval(2.0f);
        moveSequence.SetLoops(-1);
    }
}
