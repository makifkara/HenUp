using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTMP;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    PlayerMovement playerMovement;

    private void Awake()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WriteScore();
        playerMovement = FindFirstObjectByType<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        WriteScore();
    }

    void WriteScore()
    {
        if (scoreTMP == null)
        {
            return;
        }
        scoreTMP.text = "Score: " + GameManager.Instance.GetScore().ToString();
    }

    public void HandleMovementUI(float input)
    {

        if (playerMovement == null)
        {
            playerMovement = FindFirstObjectByType<PlayerMovement>();

            playerMovement.SetMoveInput(input);
            Debug.Log("Move input: " + input);
        }
        else
        {
            playerMovement.SetMoveInput(input);
            Debug.Log("Move input: " + input);
        }
    }

}
