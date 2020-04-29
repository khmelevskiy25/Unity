using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bird;

    [SerializeField] private PipeCreate creator;
    [SerializeField] private RecordHolder holder;

    private bool recordHasBeenAdded = false;

    private float aliveTime = 0.0f;

    public float AliveTime => aliveTime;

    private GameObject currentBird;
    void Update()
    {
        aliveTime += Time.deltaTime;

        var hasBird = currentBird != null;

        if (hasBird && !currentBird.GetComponent<Player>().IsDead)
            return;

        if (hasBird && currentBird.GetComponent<Player>().IsDead && !recordHasBeenAdded)
        {
            holder.AddRecord(currentBird.GetComponent<Player>().Points);
            recordHasBeenAdded = true;
        }

        if (hasBird)
            return;

        recordHasBeenAdded = false;
        aliveTime = 0.0f;
        creator.ClearPipes();
        currentBird = GameObject.Instantiate(bird, transform.position, Quaternion.identity);
    }
}
