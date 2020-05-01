using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    private void Start()
    {
        for (var index = 0; index < 3; index++)
        {
            var shift = Random.onUnitSphere * 10.0f;
            shift.y = 0;

            var enemy = Instantiate(enemyPrefab, transform.position + shift, Quaternion.identity);
        }
    }
}
