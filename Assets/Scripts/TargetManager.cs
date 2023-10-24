using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetSize { Small, Medium, Large }

public class TargetManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] targetTypes;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            SpawnAtRandom();
    }

    /// <summary>
    /// Spawns a random target at a random spawn point
    /// </summary>
    void SpawnAtRandom()
    {
        int rndTarget = Random.Range(0, targetTypes.Length);
        int rndSpawn = Random.Range(0, spawnPoints.Length);
        GameObject target = Instantiate(targetTypes[rndTarget], spawnPoints[rndSpawn].position, spawnPoints[rndSpawn].rotation);
    }
}
