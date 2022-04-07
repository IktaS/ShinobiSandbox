using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using MarchingBytes;

public class Spawner : MonoBehaviour
{
    [SerializeField] private string PoolName;
    private EntitySpawnDifficulty difficulty;
    private int spawned = 0;

    [Button("Spawn")]
    void Spawn()
    {
        if (difficulty == null) return;
        if (spawned + 1 > difficulty.max) return;
        var go = EasyObjectPool.instance.GetObjectFromPool(PoolName, transform.position, Quaternion.identity);
        var enemy = go.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.spawner = this;
            enemy.Spawn(transform.position);
            spawned++;
        }
        else
        {
            EasyObjectPool.instance.ReturnObjectToPool(go);
        }
    }

    public string GetPoolName()
    {
        return PoolName;
    }

    public void Dead(GameObject go)
    {
        EasyObjectPool.instance.ReturnObjectToPool(go);
        spawned--;
    }

    private Coroutine currentCoroutine;

    public void SetDifficulty(EntitySpawnDifficulty diff)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        difficulty = diff;
        currentCoroutine = StartCoroutine(doSpawning());
    }

    IEnumerator doSpawning()
    {
        if (difficulty == null)
        {
            yield break;
        }
        Random.InitState(difficulty.GetSeed());
        yield return new WaitForSeconds(difficulty.spawnDelay);
        Debug.Log(GetPoolName() + " spawner is online");
        for (; ; )
        {
            var rand = Random.Range(0f, 1f);
            if (rand < difficulty.chance)
            {
                Spawn();
            }
            yield return new WaitForSeconds(difficulty.spawnChanceTimer);
        }
    }
}
