using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject snakePrefab;

    [SerializeField]
    private RecordHolder holder;

    private GameObject currentSnake;

    private bool recordHasBeenAdded = false;

    private void Update()
    {
        if (currentSnake != null && !currentSnake.GetComponentInChildren<PlayerController>().IsDead)
            return;

        if (currentSnake != null)
            holder.AddRecord(currentSnake.GetComponentInChildren<PlayerController>().Points);

        currentSnake = Instantiate(snakePrefab, transform.position, transform.rotation);
        recordHasBeenAdded = true;
    }

}
