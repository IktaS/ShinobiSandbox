using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererInitializer : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform dirPoint;
    [SerializeField] private float length;

    private LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Vector3 start = startPoint.position;
        Vector3 endLine = start + (dirPoint.position - start).normalized * length;
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, endLine);
    }
}
