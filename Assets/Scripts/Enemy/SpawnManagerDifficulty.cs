using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnManagerDifficulty", menuName = "ScriptableObjects/SpawnDifficulty", order = 1)]
public class SpawnManagerDifficulty : ScriptableObject
{
    public int seed = -1;
    public EntitySpawnDifficulty wolf;
    public EntitySpawnDifficulty bat;
    public EntitySpawnDifficulty bomb;
    public EntitySpawnDifficulty woodwolf;
    public EntitySpawnDifficulty metalwolf;
    public EntitySpawnDifficulty woodbat;
    public EntitySpawnDifficulty metalbat;
    public float nextDifficultyTimer = 360f;

    public void SetSeed(int _seed)
    {
        seed = _seed;
        wolf.SetSeed(seed);
        woodwolf.SetSeed(seed);
        metalwolf.SetSeed(seed);
        bat.SetSeed(seed);
        woodbat.SetSeed(seed);
        metalbat.SetSeed(seed);
        bomb.SetSeed(seed);
    }
}
