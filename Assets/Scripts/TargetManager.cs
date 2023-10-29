using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetSize { Small, Medium, Large }

public class TargetManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] targetTypes;
    public List<GameObject> targets;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            SpawnAtRandom();
        if (Input.GetKeyDown(KeyCode.R))
            ResizeTargets();
    }

    /// <summary>
    /// Spawns a random target at a random spawn point
    /// </summary>
    void SpawnAtRandom()
    {
        int rndTarget = Random.Range(0, targetTypes.Length);
        int rndSpawn = Random.Range(0, spawnPoints.Length);
        GameObject target = Instantiate(targetTypes[rndTarget], spawnPoints[rndSpawn].position, spawnPoints[rndSpawn].rotation);
        targets.Add(target);
        ShowTargetCount();
    }

    /// <summary>
    /// Shows amount of targets in the stage
    /// </summary>
    void ShowTargetCount()
    {
        print("Number of targets: " + targets.Count);
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
    void ResizeTargets()
    {
        for (int i = 0; i <= targets.Count - 1; i++)
        {
            //changes TargetSize enum to a random state
            targets[i].GetComponent<Target>().targetSize = (TargetSize)Random.Range(0, 3);
            //reconfiures target according to it's new size
            targets[i].GetComponent<Target>().SetUp();
        }
    }
}
