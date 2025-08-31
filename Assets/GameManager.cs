using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int playerScore = 0;
    [SerializeField] private GameObject player;
    [SerializeField] public float deadZone;
    float highestY = 0f;
    float deadlyY = -10f;
    Vector3 stayPos;
    public Camera myCamera;
    PlatformSpawner platformSpawner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platformSpawner = GetComponent<PlatformSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerScore();
        CheckIfGameOver();
        if (playerScore % 5 == 0)
        {
            platformSpawner.PutThePlatformBack();
            platformSpawner.CheckSpawnCondition();
        }
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
}
