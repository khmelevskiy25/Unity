using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PlatformPlaceAttemptDelegate(Platform platform);
public delegate void PlatformPlacedDelegate(Platform platform); 
public delegate void PlatformSpawnedDelegate(Platform platform);
public delegate void OnGameStarted();
public delegate void OnGameOver();
public delegate void OnScoreChanged(int count);

public static class GameplayEvents
{
    public static event OnGameStarted OnGameStarted;
    public static event OnGameOver OnGameOver;

    public static event PlatformPlaceAttemptDelegate OnPlatformPlaceAttempted;
    public static event PlatformPlacedDelegate OnPlatformPlaced;
    public static event PlatformSpawnedDelegate OnPlatformSpawned;
    public static event OnScoreChanged OnScoreChanged;
    public static event OnScoreChanged OnMaxScoreChanged;

    public static void NotifyScoreChanged(int score)
    {
        OnScoreChanged?.Invoke(score);
    }

    public static void NotifyMaxScoreChanged(int score)
    {
        OnMaxScoreChanged?.Invoke(score);
    }

    public static void NotifyGameStarted()
    {
        OnGameStarted?.Invoke();
    }

    public static void NotifyGameOver()
    {
        OnGameOver?.Invoke();
    }

    public static void NotifyPlatformSpawned(Platform platform)
    {
        OnPlatformSpawned?.Invoke(platform);
    }

    public static void NotifyPlatformPlaced(Platform platform)
    {
        OnPlatformPlaced?.Invoke(platform);
    }

    public static void NotifyPlatformPlaceAttemptOccurred(Platform platform)
    {
        OnPlatformPlaceAttempted?.Invoke(platform);
    }
}
