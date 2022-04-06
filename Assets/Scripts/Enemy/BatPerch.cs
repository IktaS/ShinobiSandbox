using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatPerch : MonoBehaviour
{
    [SerializeField] private Vector2 radiusRange;
    [SerializeField] private float yOffset;
    [SerializeField] private int maxNode;

    public Transform trueTarget;

    private List<GameObject> nodes = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < maxNode; i++)
        {
            var randRadius = Random.Range(radiusRange.x, radiusRange.y);
            var point = Random.onUnitSphere * randRadius;
            point.y = Mathf.Abs(point.y) + yOffset;
            var node = new GameObject("Node " + i);
            nodes.Add(node);
            node.transform.position = transform.position + point;
            node.transform.SetParent(transform);
        }
    }

    public Transform GetNode()
    {
        var nodeNum = Random.Range(0, maxNode);
        return nodes[nodeNum].transform;
    }
}
