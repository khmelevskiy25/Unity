using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEnabler : MonoBehaviour
{
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        virtualCamera.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() == null)
            return;

        virtualCamera.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() == null)
            return;

        virtualCamera.gameObject.SetActive(false);
    }
}
