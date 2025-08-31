using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject basePlatform;
    [SerializeField] private float gameLimitX;
    [SerializeField] private Vector2 platformWidth;
    float spawnY = 0f;
    [SerializeField] private float spawnGap = 2.5f;
    [SerializeField] private int poolCount = 10;
    [SerializeField] private int poolTourCount = 0;
    float firstSpawnY = 0f;
    [SerializeField] private List<GameObject> platformPool = new List<GameObject>();
    int platformPoolIndex = 0;
    float difficultyMultiplier = 1f;

    GameObject lastSpawned;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        firstSpawnY = basePlatform.transform.position.y + spawnGap;
        spawnY = firstSpawnY;
        PlatformPooling();
        SpawnPlatform();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastSpawned.transform.position.y - player.transform.position.y < 3 * spawnGap)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        float spawnX = Random.Range(-gameLimitX, gameLimitX);
        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);
        if (platformPoolIndex >= platformPool.Count)
        {
            platformPoolIndex = 0;
            poolTourCount++;
            difficultyMultiplier -= poolTourCount / 100f;
        }
        if (platformPool[platformPoolIndex] == null)
        {
            Debug.Log("Platform Pool is empty or index is out of range!");
            return;
        }
        lastSpawned = platformPool[platformPoolIndex];
        lastSpawned.transform.position = spawnPos;

        lastSpawned.transform.localScale = RandomScaleByDifficulty(lastSpawned.transform);
        spawnY += spawnGap;
        platformPoolIndex++;
    }

    Vector3 RandomScaleByDifficulty(Transform transform)
    {
        float spawnScaleX = Random.Range(platformWidth.x, platformWidth.y);

        spawnScaleX *= difficultyMultiplier;

        Vector3 spawnScale = new Vector3(spawnScaleX, lastSpawned.transform.localScale.y, 1f);
        if (spawnScale.x < 1f)
        {
            return Vector3.one;
        }
        return spawnScale;
    }
    void PlatformPooling()
    {
        Vector3 poolPos = new Vector3(-50f, 0f, 0f);
        for (int i = 0; i < poolCount; i++)
        {
            var go = Instantiate(platformPrefab, poolPos, Quaternion.identity);
            platformPool.Add(go);
        }
    }
}
