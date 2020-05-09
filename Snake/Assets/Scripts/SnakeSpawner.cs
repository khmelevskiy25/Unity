using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject snakePrefab;

    private GameObject currentSnake;

    private void Update()
    {
        if (currentSnake != null)
            return;

        currentSnake = Instantiate(snakePrefab, transform.position, transform.rotation);
    }

}
