using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBonus : MonoBehaviour
{
    public float BonusAmount = 100.0f;

    private void Start()
    {
        var input = GetComponent<PlayerInput>();
        input.SpeedBonus += BonusAmount;

        Destroy(this, 2.0f);
    }

    private void OnDestroy()
    {
        var input = GetComponent<PlayerInput>();
        input.SpeedBonus -= BonusAmount;
    }
}
