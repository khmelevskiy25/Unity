using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IEndDragHandler, IDragHandler
{
    Rigidbody body;

    [SerializeField]
    private LineRenderer arrow;

    public void OnDrag(PointerEventData eventData)
    {
        arrow.enabled = true;

        var worldPosition = GetDragPosition(eventData);
        if (!worldPosition.HasValue)
            return;

        arrow.SetPosition(1, transform.position - (worldPosition.Value - transform.position));
    }

    private Vector3? GetDragPosition(PointerEventData eventData)
    {
        var screenPosition = eventData.position;

        var ray = Camera.main.ScreenPointToRay(screenPosition);
        var plane = new Plane(Vector3.up, transform.position);

        if (!plane.Raycast(ray, out float position))
            return null;

        return ray.GetPoint(position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var worldPosition = GetDragPosition(eventData);
        if (!worldPosition.HasValue)
            return;

        body.velocity = transform.position - worldPosition.Value;

        arrow.enabled = false;
    }

    private void Start()
    {
        body = GetComponent<Rigidbody>();

        arrow.enabled = false;
    }

    private void Update()
    {
        if (!arrow.enabled)
            return;

        arrow.positionCount = 2;
        arrow.SetPosition(0, transform.position);
    }
}
