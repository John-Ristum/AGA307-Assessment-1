using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetSize { Small, Medium, Large }

public class TargetManager : Singleton<TargetManager>
{
    public Transform[] spawnPoints;
    public GameObject[] targetTypes;
    public List<GameObject> targets;

    private void Start()
    {
        StartCoroutine(SpawnEnemiesWithDelay());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            SpawnAtRandom();
        if (Input.GetKeyDown(KeyCode.R))
            ResizeTargetsRandom();
    }

    /// <summary>
    /// Spawns an enemy every random amount of second
    /// </summary>
    IEnumerator SpawnEnemiesWithDelay()
    {
        for (int i = 0; i <= spawnPoints.Length - 1; i++)
        {
            int rnd = Random.Range(0, targetTypes.Length);
            GameObject target = Instantiate(targetTypes[rnd], spawnPoints[i].position, spawnPoints[i].rotation);
            SetTargetSizeDifficulty(target);
            targets.Add(target);
            ShowTargetCount();
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    /// <summary>
    /// Spawns a random target at a random spawn point
    /// </summary>
    void SpawnAtRandom()
    {
        int rndTarget = Random.Range(0, targetTypes.Length);
        int rndSpawn = Random.Range(0, spawnPoints.Length);
        GameObject target = Instantiate(targetTypes[rndTarget], spawnPoints[rndSpawn].position, spawnPoints[rndSpawn].rotation);
        SetTargetSizeDifficulty(target);
        targets.Add(target);
        ShowTargetCount();
    }

    /// <summary>
    /// Shows amount of targets in the stage
    /// </summary>
    void ShowTargetCount()
    {
        _UI.UpdateTargetCount(targets.Count);
    }

    public void DestroyTarget(GameObject _target)
    {
        //destroy selected target
        Destroy(_target);
        //remove selected target from list
        targets.Remove(_target);
        
        ShowTargetCount();
    }

    /// <summary>
    /// Get a random spawn point
    /// </summary>
    /// <returns>A random spawn point</returns>
    public Transform GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }

    /// <summary>
    /// Resizes target to a random size
    /// </summary>
    void ResizeTargetsRandom()
    {
        for (int i = 0; i <= targets.Count - 1; i++)
        {
            //changes TargetSize enum to a random state
            targets[i].GetComponent<Target>().targetSize = (TargetSize)Random.Range(0, 3);
            //reconfiures target according to it's new size
            targets[i].GetComponent<Target>().SetUp();
        }
    }

    /// <summary>
    /// Resizes all targets in array according to game difficulty
    /// </summary>
    public void ResizeAllTargetsDifficulty(int _size)
    {
        for (int i = 0; i <= targets.Count - 1; i++)
        {
            //changes TargetSize enum to match the value of the Difficulty enum
            targets[i].GetComponent<Target>().targetSize = (TargetSize)_size;
            //reconfiures target according to it's new size
            targets[i].GetComponent<Target>().SetUp();
        }
    }

    /// <summary>
    /// Sets size of a specific target according to game difficulty
    /// </summary>
    public void SetTargetSizeDifficulty(GameObject _target)
    {
        int size = 0;

        switch (_GM.difficulty)
        {
            case Difficulty.Easy:
                size = 2;
                break;
            case Difficulty.Medium:
                size = 1;
                break;
            case Difficulty.Hard:
                size = 0;
                break;
        }

        //changes TargetSize enum to match the value of the Difficulty enum
        _target.GetComponent<Target>().targetSize = (TargetSize)size;
        //reconfiures target according to it's new size
        _target.GetComponent<Target>().SetUp();
    }

    private void OnEnable()
    {
        Target.OnTargetDestroy += DestroyTarget;
    }

    private void OnDisable()
    {
        Target.OnTargetDestroy -= DestroyTarget;
    }
}
