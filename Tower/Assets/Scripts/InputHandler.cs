using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Platform currentPlatform;

    private bool isPlaying = false;

    private void Awake()
    {
        GameplayEvents.OnPlatformSpawned += OnPlatformSpawned;
        GameplayEvents.OnGameStarted += OnGameStarted;
        GameplayEvents.OnGameOver += OnGameOver;
    }

    private void OnDestroy()
    {
        GameplayEvents.OnPlatformSpawned -= OnPlatformSpawned;
        GameplayEvents.OnGameStarted -= OnGameStarted;
        GameplayEvents.OnGameOver -= OnGameOver;
    }

    private void OnGameStarted()
    {
        isPlaying = true;
    }

    private void OnGameOver()
    {
        isPlaying = false;
    }

    private void OnPlatformSpawned(Platform platform)
    {
        currentPlatform = platform;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        if (!isPlaying)
            GameplayEvents.NotifyGameStarted();
        else if (currentPlatform != null)
            GameplayEvents.NotifyPlatformPlaceAttemptOccurred(currentPlatform);
    }
}
