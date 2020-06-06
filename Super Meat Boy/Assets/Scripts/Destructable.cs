using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField]
    private GameObject[] boxes;

    private int collisionsLeft = 2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopAllCoroutines();
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(boxes[--collisionsLeft]);

        if (collisionsLeft == 0)
            Destroy(gameObject);
    }
}
