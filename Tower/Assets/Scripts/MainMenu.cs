using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup group;

    [SerializeField]
    private TMPro.TextMeshProUGUI score;

    [SerializeField]
    private TMPro.TextMeshProUGUI maxScore;

    private void Awake()
    {
        GameplayEvents.OnGameStarted += OnGameStart;
        GameplayEvents.OnGameOver += OnGameOver;
        GameplayEvents.OnScoreChanged += OnScoreChanged;
        GameplayEvents.OnMaxScoreChanged += OnMaxScoreChanged;
    }

    private void OnDestroy()
    {
        GameplayEvents.OnGameStarted -= OnGameStart;
        GameplayEvents.OnGameOver -= OnGameOver;
        GameplayEvents.OnScoreChanged -= OnScoreChanged;
        GameplayEvents.OnMaxScoreChanged -= OnMaxScoreChanged;
    }

    private void OnMaxScoreChanged(int count)
    {
        maxScore.text = count.ToString();
    }

    private void OnScoreChanged(int newScore)
    {
        score.text = newScore.ToString();

        score.transform.DOKill(true);
        score.transform.DOShakePosition(0.25f, 5.0f);
    }

    private void OnGameStart()
    {
        SetVisibilityState(false);
    }
    private void OnGameOver()
    {
        SetVisibilityState(true);
    }

    private void SetVisibilityState(bool state)
    {
        group.DOKill(true);
        group.DOFade(state ? 1.0f : 0.0f, 0.5f);
        group.blocksRaycasts = state;
    }
}
