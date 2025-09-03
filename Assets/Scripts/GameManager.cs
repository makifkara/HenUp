using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int playerScore = 0;

    [SerializeField] public float deadZone;
    float highestY = 0f;
    float deadlyY = -10f;
    Vector3 stayPos;

    [SerializeField] private Vector3 startPos;

    public static GameManager Instance { get; private set; }

    [Header("Object References")]
    [SerializeField] private GameObject playerPrefab;
    GameObject player;
    PlatformSpawner platformSpawner;
    public Camera myCamera;
    public static event Action OnGameStarted;
    public static event Action OnGameFinished;
    public static event Action OnBestScore;
    GameState gameState;
    public enum GameState
    {
        Mainmenu,
        Play,
        GameOver,
    }
    void Awake()
    {

        //Bir örnek varsa ve ben değilse, yoket. 
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
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
        //PlatformSpawner.OnPlatformSpawn += SetPlayerActive;
        //SpawnPlayer();
        //player.SetActive(false);
    }

    void SetPlayerActive()
    {
        player.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.Play)
        {

            UpdatePlayerScore();
            CheckIfGameOver();

        }

    }
    void SpawnPlayer()
    {
        if (player != null)
        {
            Destroy(player);
        }
        player = Instantiate(playerPrefab, startPos, Quaternion.identity);

    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        Debug.Log("OnSceneLoaded: " + scene.name);
        switch (scene.buildIndex)
        {
            case 0:
                gameState = GameState.Mainmenu;

                break;
            case 1:
                SpawnPlayer();
                gameState = GameState.Play;

                StartGame();
                break;
            case 2:
                gameState = GameState.GameOver;
                OnBestScore?.Invoke();
                OnGameFinished?.Invoke();
                break;
            case 3:
                gameState = GameState.Mainmenu;
                break;
            default:
                break;
        }
    }
    void Cleaner()
    {
        playerScore = 0;
        deadlyY = 0;
        highestY = 0;
    }
    void StartGame()
    {
        Cleaner();


        OnGameStarted?.Invoke();


    }


    public void LoadScene(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }
    public int GetScore()
    {
        Debug.Log("Get score: " + playerScore);
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
        if (player.transform.position.y < deadZone / 3)
        {
            //return;
        }
        if (player.transform.position.y < deadlyY)
        {
            stayPos = player.transform.position;
            GameOver();
        }
    }
    void GameOver()
    {
        UpdatePlayerScore();
        player.transform.position = stayPos;
        player.GetComponent<PlayerMovement>().enabled = false;
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        PlayerPrefs.DeleteKey("lastscore");
        PlayerPrefs.SetInt("lastscore", playerScore);

        if (PlayerPrefs.GetInt("bestscore") <= PlayerPrefs.GetInt("lastscore"))
        {
            PlayerPrefs.SetInt("bestscore", PlayerPrefs.GetInt("lastscore"));
        }

        Destroy(player);
        LoadScene(2);

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

    public GameState GetGameState()
    {
        return gameState;
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
