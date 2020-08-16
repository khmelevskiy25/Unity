using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(TogglePause);
    }
    private void TogglePause()
    {
        if(Time.timeScale > 0)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }
}
