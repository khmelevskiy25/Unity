using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    private GameObject currentSpawned;

    private void Start()
    {
        Create();

        Events.OnCleared += OnCleared;
    }

    private void OnDestroy()
    {
        Events.OnCleared -= OnCleared;
    }

    private void OnCleared()
    {
        if (currentSpawned != null)
            Destroy(currentSpawned);

        Create();
    }

    private void Create()
    {
        currentSpawned = Instantiate(prefab, transform.position, transform.rotation);
    }
}
