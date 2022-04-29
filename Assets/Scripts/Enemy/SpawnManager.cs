using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<SpawnManagerDifficulty> difficulties = new List<SpawnManagerDifficulty>();

    [SerializeField] private List<Spawner> wolfSpawners = new List<Spawner>();
    [SerializeField] private List<Spawner> woodWolfSpawners = new List<Spawner>();
    [SerializeField] private List<Spawner> metalWolfSpawners = new List<Spawner>();
    [SerializeField] private List<Spawner> batSpawners = new List<Spawner>();
    [SerializeField] private List<Spawner> woodBatSpawners = new List<Spawner>();
    [SerializeField] private List<Spawner> metalBatSpawners = new List<Spawner>();
    [SerializeField] private List<Spawner> bombSpawners = new List<Spawner>();

    [SerializeField] private List<NodeRepository> nodeRepositories = new List<NodeRepository>();

    private float elapsedTime;
    private SpawnManagerDifficulty currentDifficulty;
    private int currentDifficultyIndex = 0;

    private void Initialize()
    {
        elapsedTime = 0f;
        if (difficulties.Count > 0)
        {
            currentDifficultyIndex = 0;
            ChangeCurrentDifficulty(difficulties[currentDifficultyIndex]);
        }
    }

    void OnEnable()
    {
        wolfSpawners.ForEach((spawner) => spawner.gameObject.SetActive(true));
        woodWolfSpawners.ForEach((spawner) => spawner.gameObject.SetActive(true));
        metalWolfSpawners.ForEach((spawner) => spawner.gameObject.SetActive(true));
        batSpawners.ForEach((spawner) => spawner.gameObject.SetActive(true));
        woodBatSpawners.ForEach((spawner) => spawner.gameObject.SetActive(true));
        metalBatSpawners.ForEach((spawner) => spawner.gameObject.SetActive(true));
        bombSpawners.ForEach((spawner) => spawner.gameObject.SetActive(true));
        Initialize();
    }

    void OnDisable()
    {
        wolfSpawners.ForEach((spawner) => spawner.gameObject.SetActive(false));
        woodWolfSpawners.ForEach((spawner) => spawner.gameObject.SetActive(false));
        metalWolfSpawners.ForEach((spawner) => spawner.gameObject.SetActive(false));
        batSpawners.ForEach((spawner) => spawner.gameObject.SetActive(false));
        woodBatSpawners.ForEach((spawner) => spawner.gameObject.SetActive(false));
        metalBatSpawners.ForEach((spawner) => spawner.gameObject.SetActive(false));
        bombSpawners.ForEach((spawner) => spawner.gameObject.SetActive(false));
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (currentDifficulty != null)
        {
            if (elapsedTime >= currentDifficulty.nextDifficultyTimer)
            {
                if (difficulties.Count > currentDifficultyIndex)
                {
                    currentDifficultyIndex++;
                    ChangeCurrentDifficulty(difficulties[currentDifficultyIndex]);
                }
                elapsedTime = 0;
            }
        }
    }

    void ChangeCurrentDifficulty(SpawnManagerDifficulty difficulty)
    {
        if (difficulty == null)
        {
            return;
        }
        if (difficulty.seed < 0)
        {
            difficulty.seed = (int)System.DateTime.Now.Ticks;
        }
        Debug.Log("Difficulty " + difficulty.name + " is online");
        difficulty.SetSeed(difficulty.seed);
        nodeRepositories.ForEach((repo) => repo.SetSeed(difficulty.seed));
        wolfSpawners.ForEach((spawner) => spawner.SetDifficulty(difficulty.wolf));
        woodWolfSpawners.ForEach((spawner) => spawner.SetDifficulty(difficulty.woodwolf));
        metalWolfSpawners.ForEach((spawner) => spawner.SetDifficulty(difficulty.metalwolf));
        batSpawners.ForEach((spawner) => spawner.SetDifficulty(difficulty.bat));
        woodBatSpawners.ForEach((spawner) => spawner.SetDifficulty(difficulty.woodbat));
        metalBatSpawners.ForEach((spawner) => spawner.SetDifficulty(difficulty.metalbat));
        bombSpawners.ForEach((spawner) => spawner.SetDifficulty(difficulty.bomb));
    }
}
