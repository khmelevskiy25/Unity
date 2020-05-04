using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] private Transform buttonModel;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = buttonModel.transform.position;
    }

    public void Animate()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(buttonModel.DOMove(buttonModel.position + Vector3.down, 0.25f));
        sequence.Append(buttonModel.DOMove(initialPosition, 0.25f));
    }
}
