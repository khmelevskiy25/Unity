using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private float health = 100.0f;

    [SerializeField]
    private GameObject deathEffect;

    public void ApplyDamage(float damage)
    {
        if (health <= 0)
            return;

        health -= damage;
        Debug.Log(health);
        if (health > 0)
            return;
            
        Destroy(gameObject, 0.1f);
        var effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 4.0f);
    }
}
