using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject[] ballPrefabs; // Array to hold your three ball prefabs
    public TextMeshProUGUI ballCount;
    public int numberOfBalls = 10000; // Total number of balls to spawn
    public float spawnRadius = 0.75f; // Radius within which balls will spawn randomly
    public float spawnInterval = 0.05f; // Time interval between spawning each ball
    public int batchBallSize = 50;
    private int ballsSpawned;
    private WaitForSeconds ballSpawnTime;

    void Start()
    {
        if (ballPrefabs.Length != 3)
        {
            Debug.LogError("Please assign exactly 3 ball prefabs.");
            return;
        }
        ballsSpawned = 0;
        ballSpawnTime = new WaitForSeconds(spawnInterval);
        StartCoroutine(SpawnBallsOverTime());
    }

    void Update()
    {
        if(ballCount != null)
            ballCount.text = ballsSpawned.ToString();
    }

    IEnumerator SpawnBallsOverTime()
    {
        int batchSize = batchBallSize; // Number of balls to spawn before yielding
        for (int i = 0; i < numberOfBalls; i++)
        {
            // Determine which prefab to use (0, 1, or 2)
            int prefabIndex = i % ballPrefabs.Length;

            // Random position within a sphere of specified radius
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;

            // Instantiate the ball
            Instantiate(ballPrefabs[prefabIndex], spawnPosition, Quaternion.identity);
            ballsSpawned++;

            // Yield after a batch of balls to keep performance smooth
            if (i % batchSize == 0)
            {
                yield return ballSpawnTime; // Yield control back to the game loop
            }
        }
    }
}
