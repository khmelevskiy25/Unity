using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordHolder : MonoBehaviour
{
    List<int> records = new List<int>();

    [SerializeField] GameObject scoreWindow;

    [SerializeField] private CanvasGroup group;

    [SerializeField] Text scoreText;

    private void Start()
    {
        group.alpha = 0.0f;
    }

    public void AddRecord(int score)
    {
        records.Insert(0, score);

        records = records.GetRange(0, Mathf.Min(records.Count, 10));

        scoreText.text = string.Empty;
        foreach (var value in records)
            if (value != 0)
                scoreText.text += value + "\n"; 
    }
}
