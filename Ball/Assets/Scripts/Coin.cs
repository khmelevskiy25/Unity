using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInput>() == null)
            return;

        var sequence = DOTween.Sequence();

        sequence.Append(transform.DOMove(transform.position + Vector3.up, 0.25f));
        sequence.Append(transform.DOScale(Vector3.zero, 0.25f));
        sequence.AppendCallback(() => Destroy(gameObject));
    }

    private void Update()
    {
        transform.Rotate(Vector3.right, Time.deltaTime * 360.0f);
    }
}
