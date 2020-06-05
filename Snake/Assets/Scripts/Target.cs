using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Target : MonoBehaviour
{
    private Vector3 initialPosition;

    private Sequence moveSequence;
    private void Start()
    {
       initialPosition = transform.position;
       StartCoroutine(ScaleTarget());
       MoveTarget();
    }

    private void Update()
    {
        transform.Rotate(0, 20.0f * Time.deltaTime, 0);
    }

    private IEnumerator ScaleTarget()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            transform.DOScale(1.2f, 1.0f);
            yield return new WaitForSeconds(1);
            transform.DOScale(1f, 1.0f);
            yield return null;
        }   
    }

    private void MoveTarget()
    {
        moveSequence = DOTween.Sequence();
        moveSequence.Append(transform.DOMoveY(1, 1));
        moveSequence.AppendInterval(0.3f);
        moveSequence.Append(transform.DOMove(initialPosition, 1));
        moveSequence.SetLoops(-1);
    }
}
