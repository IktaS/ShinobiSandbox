using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntitySpawnDifficulty", menuName = "ScriptableObjects/EntitySpawn", order = 1)]
public class EntitySpawnDifficulty : ScriptableObject
{
    private int seed;
    public int max;
    public float spawnDelay;
    public float chance;
    public float spawnChanceTimer;

    public void SetSeed(int _seed)
    {
        seed = _seed;
    }

    public int GetSeed()
    {
        return seed;
    }
}
