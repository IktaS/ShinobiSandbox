using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class EarthPrisonActivator : MonoBehaviour
{
    [SerializeField] private EarthPrisonQueue _earthPrisonQueue;

    [Button]
    public void ActivateAllEarthPrison()
    {
        _earthPrisonQueue.activateAllEarthPrison();
    }
}
