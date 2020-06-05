using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField]
    private AudioSource backMusic;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(TogglePause);
    }

    private void TogglePause()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0.0f;
            backMusic.Pause();
        }
        else
        {
            Time.timeScale = 1.0f;
            backMusic.Play();
        }      
    }
}
