using System.Collections;
using UnityEngine;

public class BunnySpawner : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject bunnyPrefab;
    public float spawnInterval = 10f;
    public int maxBunnies = 3;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    private int currentBunnies = 0;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (currentBunnies < maxBunnies)
            {
                SpawnBunny();
            }
        }
    }

    void SpawnBunny()
    {
        // Pick a random spawn point
        Transform spawnPoint = spawnPoints[
            Random.Range(0, spawnPoints.Length)];

        GameObject bunny = Instantiate(
            bunnyPrefab, spawnPoint.position, Quaternion.identity);

        currentBunnies++;

        // Track when bunny is destroyed
        bunny.GetComponent<BunnyController>()
            .OnBunnyDestroyed += () => currentBunnies--;
    }
}