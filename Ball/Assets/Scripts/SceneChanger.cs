using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInput>() == null)
            return;

        SceneManager.LoadScene(sceneName);
    }
}
