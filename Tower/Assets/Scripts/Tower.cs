using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private int maxPlatformsCount = 20;

    [SerializeField]
    private float startSize = 5.0f;

    [SerializeField]
    private Platform platformPrototype;

    [SerializeField]
    private float perfectPositioningShift = 0.35f;

    [SerializeField]
    private PlatformSpawner spawner;

    private Vector2 currentSize;

    private Queue<Platform> platforms = new Queue<Platform>();

    public Vector2 CurrentSize
    {
        get
        {
            return currentSize;
        }

        set
        {
            if (value.x < 0 || value.y < 0)
                throw new Exception("Not correct size. Should be larger than zero!");

            currentSize = value;
        }
    }

    public Vector3 CurrentCenterPosition
    {
        get;
        private set;
    }

    private void Start()
    {
        ResetTower();

        GameplayEvents.OnPlatformPlaced += OnPlatformPlaced;
        GameplayEvents.OnPlatformPlaceAttempted += OnPlatformPlaceAttempt;
        GameplayEvents.OnGameStarted += OnGameStarted;
    }

    private void OnDestroy()
    {
        GameplayEvents.OnPlatformPlaced -= OnPlatformPlaced;
        GameplayEvents.OnPlatformPlaceAttempted -= OnPlatformPlaceAttempt;
        GameplayEvents.OnGameStarted -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        ResetTower();
    }

    private void OnPlatformPlaceAttempt(Platform platform)
    {
        if (TryPlacePlatform(platform))
            GameplayEvents.NotifyPlatformPlaced(platform);
    }

    private void OnPlatformPlaced(Platform platform)
    {
        MoveDown();
    }

    public void ResetTower()
    {
        transform.position = Vector3.zero;
        CurrentCenterPosition = Vector3.zero;
        CurrentSize = Vector2.one * startSize;
        while (platforms.Count > 0)
            Destroy(platforms.Dequeue().gameObject);

        var platform = spawner.Spawn(true);
        platform.transform.SetParent(transform);
        platforms.Enqueue(platform);
        transform.position = Vector3.down;
    }

    private bool TryPlacePlatform(Platform platform)
    {
        platform.StopMoving();
        var platformPosition = platform.transform.position;
        var difference = platformPosition - CurrentCenterPosition;
        difference.y = 0;
        var differenceMagnitude = difference.magnitude;
        var isPerfectPlacing = differenceMagnitude < perfectPositioningShift;
        if (Mathf.Abs(difference.x) > CurrentSize.x || Mathf.Abs(difference.z) > CurrentSize.y)
        {
            platform.Drop();
            GameplayEvents.NotifyGameOver();

            return false;
        }
        else
        {
            platform.PlayPlacingAnimation(isPerfectPlacing);
            platform.transform.SetParent(transform);

            if (isPerfectPlacing)
            {
                platformPosition.x = CurrentCenterPosition.x;
                platformPosition.z = CurrentCenterPosition.z;
                platform.transform.position = platformPosition;
            }
            else
            {
                CurrentSize = platform.Split(difference);
                CurrentCenterPosition = platform.transform.position;
            }

            platforms.Enqueue(platform);

            return true;
        }
    }

    private void MoveDown()
    {
        transform.DOKill(true);
        transform.DOMoveY(transform.position.y - 1.0f, 0.5f);
    }

    private void OnDrawGizmos()
    {
        var size = new Vector3(CurrentSize.x, 1, CurrentSize.y);
        Gizmos.DrawWireCube(CurrentCenterPosition, size);
    }
}
