using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using MarchingBytes;

public class WolfSpawner : MonoBehaviour
{
    [SerializeField] private string WolfPoolName;
    [Button("Spawn")]
    void SpawnWolf()
    {
        var go = EasyObjectPool.instance.GetObjectFromPool(WolfPoolName, transform.position, Quaternion.identity);
        var wolf = go.GetComponent<Wolf>();
        if (wolf != null)
        {
            wolf.Spawn();
        }
    }
}
