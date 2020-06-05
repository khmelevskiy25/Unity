using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

   public void OnPlayHeandler()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    public void OnExitHeandler()
    {
        Application.Quit();
    }
}
