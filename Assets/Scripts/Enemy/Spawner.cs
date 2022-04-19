using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using MarchingBytes;

public class Spawner : MonoBehaviour
{
    [SerializeField] private string PoolName;
    private EntitySpawnDifficulty difficulty;
    private Dictionary<GameObject, bool> spawnedEnemy = new Dictionary<GameObject, bool>();

    void OnDisable()
    {
        foreach (var enemy in spawnedEnemy)
        {
            EasyObjectPool.instance.ReturnObjectToPool(enemy.Key);
        }
        spawnedEnemy.Clear();
    }

    [Button("Spawn")]
    void Spawn()
    {
        if (difficulty == null) return;
        if (spawnedEnemy.Count + 1 > difficulty.max) return;
        var go = EasyObjectPool.instance.GetObjectFromPool(PoolName, transform.position, Quaternion.identity);
        var enemy = go.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.spawner = this;
            enemy.Spawn(transform.position);
            spawnedEnemy.Add(enemy.gameObject, true);
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
        spawnedEnemy.Remove(go);
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
