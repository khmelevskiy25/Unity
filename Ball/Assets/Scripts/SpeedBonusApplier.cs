using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBonusApplier : MonoBehaviour
{
    [SerializeField]
    private float amount = 100.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInput>() == null)
            return;

        var currentBonus = other.GetComponent<SpeedBonus>();
        if (currentBonus != null)
            Destroy(currentBonus);

        var bonus = other.gameObject.AddComponent<SpeedBonus>();
        bonus.BonusAmount = amount;
    }
}
