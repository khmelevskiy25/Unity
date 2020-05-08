using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lift : MonoBehaviour
{
    [SerializeField]
    private Transform targetPosition;

    [SerializeField]
    private Rigidbody body;

    private Vector3 initialPosition;

    private Sequence moveSequence;

    private void Start()
    {
        initialPosition = transform.position;
    }

    public void Move()
    {
        if (moveSequence != null)
            return;

        moveSequence = DOTween.Sequence();
        moveSequence.Append(body.DOMove(targetPosition.position, 6.0f));
        moveSequence.AppendInterval(2.0f);
        moveSequence.Append(body.DOMove(initialPosition, 6.0f));
        moveSequence.AppendCallback(() => moveSequence = null);
    }
}
