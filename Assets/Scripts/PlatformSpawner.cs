using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    //GameObject player;
    [SerializeField] private GameObject basePlatformPrefab;
    GameObject basePlatform;
    [SerializeField] private Vector3 baseSpawn;
    [SerializeField] private float gameLimitX;
    [SerializeField] private Vector2 platformWidth;
    float spawnY = 0f;
    [SerializeField] private float spawnGap = 2.5f;
    [SerializeField] private int poolCount = 10;
    [SerializeField] private int poolTourCount = 0;
    float firstSpawnY = 0f;
    [SerializeField] private List<GameObject> platformPool = new List<GameObject>();
    Vector3 poolPos = Vector3.left * 100f;
    int platformPoolIndex = 0;
    float difficultyMultiplier = 1f;
    [SerializeField] private float minScaleX = 0f;

    GameObject lastSpawned;
    Vector3 lastSpawnPos = Vector3.zero;
    int basePlatformSpawned = 0;
    public static Action OnPlatformSpawn;
    [SerializeField] private float powerUpPlatformRate = 3f;
    [SerializeField] private List<GameObject> powerUpPrefabs = new List<GameObject>();
    List<GameObject> powerUps = new List<GameObject>();
    bool shouldSpawn = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platformPool.Clear();


    }
    private void OnEnable()
    {
        GameManager.OnGameStarted += SpawnPlatform;
        GameManager.OnGameFinished += PutAllPlatformsBack;
    }
    void OnDisable()
    {
        GameManager.OnGameStarted -= SpawnPlatform;
        GameManager.OnGameFinished -= PutAllPlatformsBack;
    }

    // Update is called once per frame
    void Update()
    {
        HandleGameState();
    }
    void HandleGameState()
    {
        switch (GameManager.Instance.GetGameState())
        {
            case GameManager.GameState.Mainmenu:
                PlatformPooling();

                break;
            case GameManager.GameState.Play:

                if ((int)CameraFollow.Instance.GetCameraPosition().y % 5 == 0)
                {
                    PutThePlatformBack();
                    CheckSpawnCondition();
                }
                break;
            case GameManager.GameState.GameOver:
                Cleaner();


                break;
            default:
                break;
        }
    }

    void PutAllPlatformsBack()
    {
        basePlatform.transform.position = poolPos;
        foreach (GameObject platform in platformPool)
        {
            platform.transform.position = poolPos;
        }

    }
    void Cleaner()
    {
        lastSpawned = null;
        basePlatformSpawned = 0;
        firstSpawnY = 0;
        spawnY = 0;
        lastSpawnPos = Vector3.zero;
        platformPoolIndex = 0;
        poolTourCount = 0;
        shouldSpawn = true;

    }
    public void CheckSpawnCondition()
    {
        if (lastSpawnPos == Vector3.zero)
        {

            // return;
        }

        //float playerGap = lastSpawnPos.y - GameManager.Instance.GetPlayerObject().transform.position.y;
        float cameraGap = lastSpawnPos.y - CameraFollow.Instance.GetCameraPosition().y;
        if (cameraGap < 10 * spawnGap)
        {
            for (int i = 0; i < 40; i++)
            {
                shouldSpawn = true;
                SpawnPlatform();
                if (i % powerUpPlatformRate == 0)
                {
                    GameObject powerUpGO;
                    powerUpGO = Instantiate(powerUpPrefabs[1]);
                    powerUps.Add(powerUpGO);
                    powerUpGO.transform.position = new Vector3(lastSpawnPos.x, lastSpawnPos.y + 0.5f, lastSpawnPos.z);
                }
            }

        }
    }
    public void PutThePlatformBack()
    {
        if (CameraFollow.Instance.GetCameraPosition().y - baseSpawn.y >= GameManager.Instance.deadZone * 1.5)
        {
            basePlatform.transform.position = poolPos;
        }
        foreach (GameObject platform in platformPool)
        {
            if (CameraFollow.Instance.GetCameraPosition().y - platform.transform.position.y >= GameManager.Instance.deadZone * 1.5)
            {
                platform.transform.position = poolPos;
            }
        }

    }
    void SpawnPlatform()
    {
        if (!shouldSpawn)
        {
            return;
        }
        if (basePlatformSpawned == 0)
        {
            basePlatform.transform.position = baseSpawn;
            firstSpawnY = basePlatform.transform.position.y + spawnGap;
            spawnY = firstSpawnY;
            OnPlatformSpawn?.Invoke();
            lastSpawned = basePlatform;
            //player = GameManager.Instance.GetPlayerObject();
            basePlatformSpawned = 1;

        }

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

            return;
        }
        lastSpawned = platformPool[platformPoolIndex];

        lastSpawned.transform.position = spawnPos;
        lastSpawnPos = lastSpawned.transform.position;
        lastSpawned.transform.localScale = RandomScaleByDifficulty(lastSpawned.transform);
        spawnY += spawnGap;
        platformPoolIndex++;

        shouldSpawn = false;
    }

    Vector3 RandomScaleByDifficulty(Transform transform)
    {
        float spawnScaleX = Random.Range(platformWidth.x, platformWidth.y);

        spawnScaleX *= difficultyMultiplier;
        if (spawnScaleX < minScaleX)
        {
            spawnScaleX = minScaleX;
        }
        Vector3 spawnScale = new Vector3(spawnScaleX, lastSpawned.transform.localScale.y, 1f);

        return spawnScale;
    }
    void PlatformPooling()
    {
        if (platformPool.Count > 0)
        {
            return;
        }

        basePlatform = Instantiate(basePlatformPrefab, transform);
        basePlatform.transform.position = poolPos;
        basePlatformSpawned = 0;
        for (int i = 0; i < poolCount; i++)
        {
            var go = Instantiate(platformPrefab, transform);
            go.transform.position = poolPos;
            platformPool.Add(go);
        }
    }
}
