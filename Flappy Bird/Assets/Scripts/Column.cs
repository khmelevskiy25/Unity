using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    private float speed;
    private BirdSpawner spawner;

    public void Init(BirdSpawner spawner, float speed)
    {
        this.speed = speed;
        this.spawner = spawner;
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime * Mathf.Max(spawner.AliveTime, 1.0f));
    }
}
