using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EarthPrisonQueue", menuName = "ScriptableObjects/Queue/EarthPrisonQueue", order = 1)]
public class EarthPrisonQueue : ScriptableObject
{
    public int maximumSize = 1;
    private Queue<EarthPrison> queue = new Queue<EarthPrison>();

    public bool addEarthPrison(EarthPrison ep)
    {
        if (queue.Count < maximumSize)
        {
            queue.Enqueue(ep);
            return true;
        }
        return false;
    }

    public void activateAllEarthPrison()
    {
        while (queue.Count > 0)
        {
            EarthPrison prison = queue.Dequeue();
            prison.Activate();
        }
    }
}
