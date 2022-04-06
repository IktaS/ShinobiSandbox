using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using MarchingBytes;

public class Spawner : MonoBehaviour
{
    [SerializeField] private string PoolName;
    [Button("Spawn")]
    void Spawn()
    {
        var go = EasyObjectPool.instance.GetObjectFromPool(PoolName, transform.position, Quaternion.identity);
        var enemy = go.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Spawn(transform.position);
        }
    }
}
