using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordHolder : MonoBehaviour
{
    List<int> records = new List<int>();

    [SerializeField] Text scoreText;

    private void Start()
    {
        scoreText.gameObject.SetActive(false);
    }

    public void AddRecord (int score)
    {
        records.Insert(0, score);

        records = records.GetRange(0, Mathf.Min(records.Count, 10));

        scoreText.text = string.Empty;
        foreach (var value in records)
            scoreText.text += value + "\n";

        scoreText.gameObject.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(Disable());
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(1.0f);
        scoreText.gameObject.SetActive(false);
    }
}
