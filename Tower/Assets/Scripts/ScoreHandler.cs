using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    private static readonly string MaxScoreName = "MaxScore";

    private int score;

    private int MaxScore
    {
        get
        {
            return PlayerPrefs.GetInt(MaxScoreName, 0);
        }

        set
        {
            if (MaxScore > value)
                throw new Exception("The current max score is larger than that you are trying to set");

            PlayerPrefs.SetInt(MaxScoreName, value);
            GameplayEvents.NotifyMaxScoreChanged(value);
        }
    }

    private void Start()
    {
        GameplayEvents.OnPlatformPlaced += AddScore;
        GameplayEvents.OnGameStarted += ResetScore;

        GameplayEvents.NotifyMaxScoreChanged(MaxScore);
    }

    private void OnDestroy()
    {
        GameplayEvents.OnPlatformPlaced -= AddScore;
        GameplayEvents.OnGameStarted -= ResetScore;
    }

    private void ResetScore()
    {
        score = 0;
        GameplayEvents.NotifyScoreChanged(score);
    }

    private void AddScore(Platform platform)
    {
        score++;
        GameplayEvents.NotifyScoreChanged(score);

        if (score > MaxScore)
            MaxScore = score;
    }
}
