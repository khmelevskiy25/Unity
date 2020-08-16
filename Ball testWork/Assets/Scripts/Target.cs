using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private static HashSet<Target> targets = new HashSet<Target>();

    private void Start()
    {
        targets.Add(this);
    }

    private void OnDestroy()
    {
        targets.Remove(this);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Ball>() == null)
            return;

        Destroy(gameObject);
        if (targets.Count == 1)
            Events.NotifyAllTargetsCleared();
    } 
}
