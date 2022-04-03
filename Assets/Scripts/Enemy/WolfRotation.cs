using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfRotation : MonoBehaviour
{
    [SerializeField] private int MaxNodeNum;
    [SerializeField] private float radius;
    [SerializeField] private float rotationRate;

    public Transform trueTarget;

    private List<GameObject> nodes = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < MaxNodeNum; i++)
        {
            var node = new GameObject("Node " + i);
            nodes.Add(node);
            node.transform.position = transform.position + new Vector3(radius, 0, 0);
            node.transform.SetParent(transform);
            transform.RotateAround(transform.position, Vector3.up, 360 / MaxNodeNum);
        }
    }

    public Transform GetNode()
    {
        var nodeNum = Random.Range(0, MaxNodeNum);
        return nodes[nodeNum].transform;
    }

    void FixedUpdate()
    {
        transform.RotateAround(transform.position, Vector3.up, rotationRate);
    }
}
