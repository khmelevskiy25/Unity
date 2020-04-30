using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    [SerializeField]
    private Transform shootPosition;

    [SerializeField]
    private float shootForce = 0.5f;

    [SerializeField]
    private float reloadDuration = 1.5f;

    private float nextPossibleShotTime = 0.0f;

    [SerializeField]
    private GameObject hitPrefab;

    [SerializeField]
    private float shootImpulse = 1000.0f;

    [SerializeField]
    private ParticleSystem muzzleFlash;

    private Rigidbody body;

    private void Start()
    {
        body = GetComponentInParent<Rigidbody>();
    }

    private void Update()
    {
        if (Time.time < nextPossibleShotTime)
            return;

        if (!Input.GetMouseButtonDown(0))
            return;

        body.AddForceAtPosition(-shootPosition.forward * shootForce, shootPosition.position, ForceMode.Acceleration);

        nextPossibleShotTime = Time.time + reloadDuration;

        muzzleFlash.Stop();
        muzzleFlash.Play();

        var ray = new Ray(shootPosition.position, shootPosition.forward);
        if (!Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
            return;

        var effect = Instantiate(hitPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(effect, 2.0f);

        var radius = 10.0f;
        var overlapped = Physics.OverlapSphere(hit.point, radius);

        foreach (var item in overlapped)
        {
            var rb = item.GetComponent<Rigidbody>();
            if (rb == null)
                continue;

            rb.AddExplosionForce(shootImpulse * rb.mass, hit.point, radius);
        }
    }
}
