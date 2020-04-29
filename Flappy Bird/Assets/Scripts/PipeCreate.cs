using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCreate : MonoBehaviour
{
    [SerializeField] private BirdSpawner spawner;

    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject pipe;

    [SerializeField]
    private float minPassedAmount = 2.0f;

    [SerializeField]
    private float maxShift = 2.0f;

    private float passedAmount = 0.0f;

    private List<GameObject> createdPipes = new List<GameObject>();

    private void Update()
    {
        passedAmount += Time.deltaTime * Mathf.Max(spawner.AliveTime, 1.0f);

        if (passedAmount < minPassedAmount)
            return;

        passedAmount = 0.0f;

        var created = GameObject.Instantiate(pipe, transform.position + Vector3.up * Random.Range(-maxShift, maxShift), Quaternion.identity);
        createdPipes.Add(created);

        created.GetComponent<Column>().Init(spawner, speed);

        Destroy(created, 10);
    }

    public void ClearPipes()
    {
        foreach (var pipeToDelete in createdPipes)
            Destroy(pipeToDelete);

        createdPipes.Clear();
    }
}
