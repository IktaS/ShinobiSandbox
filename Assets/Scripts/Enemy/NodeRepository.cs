using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeRepository : MonoBehaviour
{
    protected int seed;
    [SerializeField] protected int MaxNodeNum;
    [SerializeField] protected Transform trueTarget;
    protected List<GameObject> nodes = new List<GameObject>();

    public Transform GetTrueTarget()
    {
        return trueTarget;
    }

    public virtual void SetSeed(int _seed)
    {
        seed = _seed;
        Random.InitState(seed);
        GenerateNodes();
    }

    public Transform GetNode()
    {
        var nodeNum = Random.Range(0, MaxNodeNum);
        if (nodeNum > nodes.Count)
        {
            return null;
        }
        return nodes[nodeNum].transform;
    }

    public virtual void GenerateNodes()
    {

    }
}
