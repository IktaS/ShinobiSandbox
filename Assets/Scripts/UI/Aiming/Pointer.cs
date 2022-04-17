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
        _lr.SetPosition(0, direction.position);
        var end = RaycastHit();
        _lr.SetPosition(1, end);
        reticle.transform.position = end;
    }

    private Vector3 RaycastHit()
    {
        RaycastHit hit = CreateForwardRaycast();
        reticle.SetActive(false);
        Vector3 end = DefaultEnd(defaultLength);

        if (hit.collider == null)
        {
            pointed?.WhenUnpointed(this);
            pointed = null;
            return end;
        }
        end = hit.point;
        reticle.SetActive(true);

        var newPointable = hit.collider.gameObject.GetComponent<IPointable>();
        if (newPointable == null)
        { // When nothing is pointed and something is pointed before
            pointed?.WhenUnpointed(this);
            pointed = null;
            return end;
        }
        if (newPointable != null)
        { // When something is pointed
            if (pointed == null)
            { // if nothing is pointed before
                pointed = newPointable;
                pointed.WhenPointed(this);
                return end;
            }
            if (pointed == newPointable)
            {
                return end;
            }
            if (pointed != newPointable)
            { // if something is selected before and it's different
                pointed.WhenUnpointed(this);
                pointed = newPointable;
                pointed.WhenPointed(this);
                return end;
            }
        }
        // When nothing is pointed and nothing is pointed before, do nothing.
        // When something is pointed and it's the same as before, do nothing.

        return end;
    }

    private RaycastHit CreateForwardRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(direction.position, (direction.position - start.position).normalized);
        Debug.DrawRay(direction.position, (direction.position - start.position).normalized);

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