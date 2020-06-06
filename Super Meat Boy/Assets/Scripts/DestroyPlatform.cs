using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatform : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Collider2D collider;

    private bool isDestroying = false;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        var initialPosition = transform.position;
        var spawner = FindObjectOfType<Spawner>();
        spawner.OnRestart += () =>
            {
                isDestroying = false;
                collider.enabled = true;
                spriteRenderer.DOKill(true);
                spriteRenderer.color = Color.white;
                transform.DOKill(true);
                transform.position = initialPosition;
                StopAllCoroutines();
                gameObject.SetActive(true);
            };
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDestroying)
            return;

        isDestroying = true;

        StopAllCoroutines();
        StartCoroutine(DestroyGround());
    }

    private IEnumerator DestroyGround()
    {
        yield return new WaitForSeconds(0.5f);
        collider.enabled = false;
        spriteRenderer.DOFade(0.0f, 0.5f);
        transform.DOMove(transform.position + Vector3.down * 5.0f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
