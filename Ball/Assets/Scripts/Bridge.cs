using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bridge : MonoBehaviour
{
    private Vector3 initialRotation;

    private bool isDown;

    private void Start()
    {
        initialRotation = transform.localEulerAngles;
    }

    public void ToggleState()
    {
        transform.DOKill(false);
        transform.DOLocalRotate(isDown ? initialRotation : Vector3.zero, 0.5f).SetEase(Ease.OutBounce, 0.2f);
        isDown = !isDown;
    }

    public void MoveUp()
    {
        if (!isDown)
            return;

        ToggleState();
    }
}
