using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundHandler : MonoBehaviour
{
    [SerializeField]
    private float colorStep = 0.1f;

    [SerializeField]
    private PlatformSpawner spawner;

    [SerializeField]
    private Material materialToChange;

    [SerializeField]
    private Camera camera;

    private static readonly int firstColorHash = Shader.PropertyToID("_FirstColor");
    private static readonly int secondColorHash = Shader.PropertyToID("_SecondColor");

    private void Start()
    {
        SetToCurrentGradient();
    }

    private void SetToCurrentGradient()
    {
        var firstColor = spawner.CurrentGradient.Evaluate(spawner.CurrentColorPosition);
        var secondColorPosition = spawner.CurrentColorPosition + colorStep;
        if (secondColorPosition > 1.0f)
            secondColorPosition -= 1.0f;

        var secondColor = spawner.CurrentGradient.Evaluate(secondColorPosition);

        materialToChange.SetColor(firstColorHash, firstColor);
        materialToChange.SetColor(secondColorHash, secondColor);
    }

    private void Update()
    {
        transform.localScale = Vector3.one * camera.orthographicSize * 2.0f;

        var firstColor = spawner.CurrentGradient.Evaluate(spawner.CurrentColorPosition);
        var secondColorPosition = spawner.CurrentColorPosition + colorStep;
        if (secondColorPosition > 1.0f)
            secondColorPosition -= 1.0f;

        var secondColor = spawner.CurrentGradient.Evaluate(secondColorPosition);

        var firstColorToSet = Color.Lerp(materialToChange.GetColor(firstColorHash), firstColor, Time.deltaTime);
        var secondColorToSet = Color.Lerp(materialToChange.GetColor(secondColorHash), secondColor, Time.deltaTime);
        materialToChange.SetColor(firstColorHash, firstColorToSet);
        materialToChange.SetColor(secondColorHash, secondColorToSet);
    }
}
