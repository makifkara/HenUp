using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int playerScore = 0;

    [SerializeField] public float deadZone;
    float highestY = 0f;
    float deadlyY = -10f;
    Vector3 stayPos;
    bool isGameOn = false;
    [SerializeField] private Vector3 startPos;

    public static GameManager Instance { get; private set; }

    [Header("Object References")]
    [SerializeField] private GameObject playerPrefab;
    GameObject player;
    PlatformSpawner platformSpawner;
    public Camera myCamera;

    void Awake()
    {

        //Bir örnek varsa ve ben değilse, yoket. 
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platformSpawner = GetComponent<PlatformSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOn)
        {

            UpdatePlayerScore();
            CheckIfGameOver();
            if (playerScore % 5 == 0)
            {
                platformSpawner.PutThePlatformBack();
                platformSpawner.CheckSpawnCondition();
            }
        }

    }
    void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, startPos, Quaternion.identity);

    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        if (scene.buildIndex == 1) // Game scene
        {

            GameOnRoutine();

        }
        else
        {
            GameOffRoutine();
        }
    }
    void GameOnRoutine()
    {
        isGameOn = true;
        SpawnPlayer();
        platformSpawner.enabled = true;
    }
    void GameOffRoutine()
    {
        isGameOn = false;
        //platformSpawner.enabled = false;
    }

    public void LoadScene(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }
    public int GetScore()
    {
        return playerScore;
    }
    public float GetHighestY()
    {
        return highestY;
    }
    void CheckIfGameOver()
    {
        if (player == null)
        { return; }
        if (player.transform.position.y < deadlyY)
        {
            stayPos = player.transform.position;
            GameOver();
        }
    }
    void GameOver()
    {
        player.transform.position = stayPos;
        player.GetComponent<PlayerMovement>().enabled = false;
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    void UpdatePlayerScore()
    {
        float currentY = player.transform.position.y;

        if (highestY < currentY)
        {
            highestY = currentY;
            deadlyY = highestY - deadZone;
        }
        playerScore = (int)highestY;
    }

    public bool IsGameOn()
    {
        return isGameOn;
    }
    public GameObject GetPlayerObject()
    {
        if (player == null)
        {
            return null;
        }
        return player;
    }
    public PlatformSpawner GetPlatformSpawner()
    {
        if (platformSpawner == null)
        {
            return null;
        }
        return platformSpawner;
    }

}
