using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Pointer : MonoBehaviour
{
    [SerializeField] private GameObject reticle;
    [SerializeField] private float defaultLength;
    [SerializeField] private Transform start;
    [SerializeField] private Transform direction;
    [SerializeField] private LayerMask mask;

    private LineRenderer _lr;
    private IPointable pointed;

    void Start()
    {
        _lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        UpdateStartEnd();
        if (pointed != null)
        {
            pointed.WhenStay(this);
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void UpdateStartEnd()
    {
        _lr.SetPosition(0, start.position);
        var end = RaycastHit();
        _lr.SetPosition(1, end);
        reticle.transform.position = end;
    }

    private Vector3 RaycastHit()
    {
        RaycastHit hit = CreateForwardRaycast();
        reticle.SetActive(false);
        Vector3 end = DefaultEnd(defaultLength);

        if (hit.collider != null)
        {
            end = hit.point;
            reticle.SetActive(true);

            pointed = hit.collider.gameObject.GetComponent<IPointable>();
            if (pointed != null)
            {
                pointed.WhenPointed(this);
            }
        }
        else
        {
            if (pointed != null)
            {
                pointed.WhenUnpointed(this);
                pointed = null;
            }
        }

        return end;
    }

    private RaycastHit CreateForwardRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(start.position, direction.position);

        Physics.Raycast(ray, out hit, defaultLength, mask);
        return hit;
    }

    private Vector3 DefaultEnd(float length)
    {
        return start.position + (direction.position - start.position).normalized * length;
    }
}

public interface IPointable
{
    public void WhenPointed(Pointer p);
    public void WhenStay(Pointer p);
    public void WhenUnpointed(Pointer p);
}