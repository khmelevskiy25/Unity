using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class Platform : MonoBehaviour
{
    [SerializeField]
    private float moveDuration = 1.0f;

    [SerializeField]
    private MeshRenderer renderer;

    [SerializeField]
    private Transform placeEffect;

    [SerializeField]
    private Rigidbody rigidBody;

    private Sequence moveSequence = null;

    public Vector2 CurrentSize
    {
        get
        {
            var scale = transform.localScale;
            return new Vector2(scale.x * 0.5f, scale.z * 0.5f);
        }
    }

    private void Start()
    {
        placeEffect.localScale = Vector3.one;
        placeEffect.gameObject.SetActive(false);
    }

    public void Drop()
    {
        rigidBody.isKinematic = false;

        transform.DOScale(0.0f, 0.5f)
            .SetDelay(1.0f)
            .OnComplete(() => Destroy(gameObject));
    }

    public void StartMovement(Vector3 firstPoint, Vector3 secondPoint)
    {
        if (moveSequence != null)
            throw new Exception("The platform is already moving");

        moveSequence = DOTween.Sequence();

        moveSequence.Append(transform.DOMove(firstPoint, moveDuration).SetEase(Ease.Linear));
        moveSequence.Append(transform.DOMove(secondPoint, moveDuration).SetEase(Ease.Linear));

        moveSequence.SetLoops(-1);
    }

    public void StopMoving()
    {
        moveSequence.Kill(false);
        moveSequence = null;
    }

    public void PlayPlacingAnimation(bool isPerfect)
    {
        transform.DOKill(false);
        transform.DOShakePosition(0.3f, 0.3f);
        if (!isPerfect)
            return;

        placeEffect.gameObject.SetActive(true);
        var placeEffectSequence = DOTween.Sequence();

        var localScale = transform.localScale;
        var invertedScale = new Vector3(1.0f / localScale.x, 1.0f / localScale.z, 1);

        placeEffectSequence.Append(placeEffect.DOScale(Vector3.one + invertedScale * 0.3f, 0.25f));
        placeEffectSequence.Append(placeEffect.DOScale(1f, 0.25f));
        placeEffectSequence.AppendCallback(() => placeEffect.gameObject.SetActive(false));
    }

    public void SetSize(Vector2 size)
    {
        transform.localScale = new Vector3(size.x, 1.0f, size.y);
    }

    public Vector3 Split(Vector3 difference)
    {
        var initialSize = CurrentSize * 2.0f;
        var currentPlatformSize = initialSize;
        var absDifference = new Vector3(Mathf.Abs(difference.x), 0, Mathf.Abs(difference.z));

        currentPlatformSize.x -= absDifference.x;
        currentPlatformSize.y -= absDifference.z;

        var splitedSize = new Vector2(absDifference.x, absDifference.z);
        if (absDifference.x > absDifference.z)
            splitedSize.y = currentPlatformSize.y;
        else
            splitedSize.x = currentPlatformSize.x;

        transform.position -= difference * 0.5f;

        var normalizedDifference = difference.normalized;
        var splitedPart = Instantiate(this, transform.parent);
        var requiredShiftAmount = Mathf.Abs(Vector3.Dot(normalizedDifference, new Vector3(initialSize.x, 0, initialSize.y))) * 0.5f;
        splitedPart.transform.position += normalizedDifference * requiredShiftAmount;

        splitedPart.SetSize(splitedSize);

        splitedPart.Drop();

        SetSize(currentPlatformSize);

        return currentPlatformSize;
    }

    public void SetColor(Color color)
    {
        renderer.material.color = color;
    }
}
