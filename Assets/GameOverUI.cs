using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTMP;
    [SerializeField] private TextMeshProUGUI bestscoreTMP;

    int lastscore;
    int bestscore;
    int counter = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bestscoreTMP.color = Color.white;
        GameManager.OnGameFinished += UpdateScores;
        GameManager.OnBestScore += GotBestScore;
        counter = 0;
    }
    void OnDestroy()
    {
        GameManager.OnGameFinished -= UpdateScores;
        GameManager.OnBestScore -= GotBestScore;
    }
    private void Update()
    {
        if (GameManager.Instance.GetGameState() == GameManager.GameState.GameOver)
        {
            UpdateScores();
        }
    }
    public void UpdateScores()
    {
        if (counter > 0)
        {
            return;
        }
        Debug.Log("Score updated on gameoverui");
        lastscore = PlayerPrefs.GetInt("lastscore");
        scoreTMP.text = "SCORE: " + lastscore.ToString();

        bestscore = PlayerPrefs.GetInt("bestscore");
        bestscoreTMP.text = "BEST SCORE: " + bestscore.ToString();
        counter++;
    }
    void GotBestScore()
    {
        bestscoreTMP.color = Color.green;
    }
}
