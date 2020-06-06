using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Spawner : MonoBehaviour
{
    public event Action OnRestart;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private int jumpsCount = 10;

    [SerializeField]
    private CinemachinePathBase path;

    private GameObject currentPlayer;

    private void Start()
    {
        Create();
    }

    private void Create()
    {
        currentPlayer = Instantiate(player, transform.position, transform.rotation);
        currentPlayer.GetComponent<PlayerController>().JumpsCount = jumpsCount;
        var virtualCamera = currentPlayer.GetComponentInChildren<CinemachineVirtualCamera>();

        var transposer = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        transposer.m_Path = path;

        if (OnRestart != null)
            OnRestart();
    }

    private void Update()
    {
        if (currentPlayer != null)
            return;

        Create();
    }
}
