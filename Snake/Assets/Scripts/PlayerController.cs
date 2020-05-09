using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private List<Transform> tails;

    [Range(0, 4)]
    [SerializeField]
    private float bonesDistance;

    [SerializeField]
    private GameObject bonePrefab;

    [Range(0, 20)]
    [SerializeField]
    private float speed;

    [SerializeField]
    private float force;

    private Transform _transform;

    private Rigidbody body;

    private Collider ourCollider;


    private void Start()
    {
        _transform = GetComponent<Transform>();
        body = GetComponent<Rigidbody>();
        ourCollider = GetComponent<Collider>();
        StartCoroutine(ChangeSpeed());
    }

    private void FixedUpdate()
    {
        MoveSnake(_transform.position + transform.forward * speed * Time.deltaTime);

        var angel = Input.GetAxis("Horizontal") * force * Time.deltaTime;
        body.MoveRotation(Quaternion.Euler(0, angel, 0) * body.rotation);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!Physics.ComputePenetration(collision.collider, collision.collider.transform.position, collision.collider.transform.rotation,
            ourCollider, transform.position, transform.rotation,
            out Vector3 normal,
            out float distance))
            return;

        normal.y = 0;
        normal.Normalize();

        body.transform.position = body.transform.position - normal * distance;
    }

    private void MoveSnake(Vector3 newPosition)
    {
        Vector3 prevPosition = _transform.position;
        body.MovePosition(newPosition);

        var previousBoneTransform = transform;
        foreach(var bone in tails)
        {
            bone.LookAt(previousBoneTransform);

            var difference = bone.transform.position - previousBoneTransform.position;
            bone.transform.position = previousBoneTransform.position + difference.normalized * bonesDistance;

            previousBoneTransform = bone.transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Target>() != null)
        {
            Destroy(other.gameObject);

            var lastBone = tails[tails.Count - 1];

            var bone = Instantiate(bonePrefab, transform.root);
            bone.transform.position = lastBone.transform.position - lastBone.transform.forward * bonesDistance;
            tails.Add(bone.transform);
        }

        if (other.GetComponent<BoneScript>() != null)
            Destroy(transform.root.gameObject);
    }

    private IEnumerator ChangeSpeed()
    {
        while(true)
        {
            yield return new WaitForSeconds(2.0f);

            speed *= 2.0f;
            if (speed > 20)
                speed = 20;
        }
    }
}
