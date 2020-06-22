using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    [SerializeField]
    private GameObject menuCamera;

    [SerializeField]
    private GameObject gameplayCamera;

    private void Start()
    {
        SetCamerasState(true);

        GameplayEvents.OnGameStarted += OnGameStart;
        GameplayEvents.OnGameOver += OnGameOver;
    }

    private void OnDestroy()
    {
        GameplayEvents.OnGameStarted -= OnGameStart;
        GameplayEvents.OnGameOver -= OnGameOver;
    }

    private void OnGameStart()
    {
        SetCamerasState(false);
    }

    private void OnGameOver()
    {
        SetCamerasState(true);
    }

    private void SetCamerasState(bool isMenu)
    {
        gameplayCamera.SetActive(!isMenu);
        menuCamera.SetActive(isMenu);
    }
}
