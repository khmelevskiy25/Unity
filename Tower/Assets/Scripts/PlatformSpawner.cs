using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField]
    private Tower tower;

    [SerializeField]
    private List<Gradient> gradients;

    [SerializeField]
    private Platform platformPrototype;

    [SerializeField]
    private float spawnDistance = 5.0f;

    [SerializeField]
    private float platformColorStep = 0.1f;

    private float currentColorPosition = 0.0f;

    private Gradient currentGradient;

    public Gradient CurrentGradient => currentGradient;

    public float CurrentColorPosition => currentColorPosition;

    private bool isFirstAttempt = true;

    private static List<Vector3> directions = new List<Vector3> { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };

    private void Awake()
    {
        GameplayEvents.OnPlatformPlaced += OnPlatformPlaced;
        GameplayEvents.OnGameStarted += OnGameStarted;
        currentGradient = gradients[Random.Range(0, gradients.Count)];
    }

    private void OnDestroy()
    {
        GameplayEvents.OnPlatformPlaced -= OnPlatformPlaced;
        GameplayEvents.OnGameStarted -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        if (!isFirstAttempt)
        {
            currentGradient = gradients[Random.Range(0, gradients.Count)];
            currentColorPosition = 0.0f;
        }

        isFirstAttempt = false;

        StopAllCoroutines();
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.3f);
        Spawn(false);
    }

    private void OnPlatformPlaced(Platform platform)
    {
        Spawn(false);
    }

    public Platform Spawn(bool initialSpawn)
    {
        var directionIndex = Random.Range(0, directions.Count);
        var direction = initialSpawn ? Vector3.zero : directions[directionIndex];
        var startPosition = (transform.position + direction * spawnDistance) + tower.CurrentCenterPosition;
        var endPosition = (transform.position - direction * spawnDistance) + tower.CurrentCenterPosition;
        var spawnedPlatform = Instantiate(platformPrototype, startPosition, Quaternion.identity);
        
        spawnedPlatform.SetSize(tower.CurrentSize);

        if (!initialSpawn)
            spawnedPlatform.StartMovement(endPosition, startPosition);

        spawnedPlatform.SetColor(currentGradient.Evaluate(currentColorPosition));
        currentColorPosition += platformColorStep;
        if (currentColorPosition > 1.0f)
            currentColorPosition -= 1.0f;

        if (!initialSpawn)
            GameplayEvents.NotifyPlatformSpawned(spawnedPlatform);

        return spawnedPlatform;
    }
}
