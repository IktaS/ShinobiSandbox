using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatPerch : NodeRepository
{
    [SerializeField] private Vector2 radiusRange;
    [SerializeField] private float yOffset;

    void Start()
    {

    }

    public override void SetSeed(int _seed)
    {
        seed = _seed;
        Random.InitState(seed);
        GenerateNodes();
    }

    public override void GenerateNodes()
    {
        base.GenerateNodes();
        for (int i = 0; i < MaxNodeNum; i++)
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
}
