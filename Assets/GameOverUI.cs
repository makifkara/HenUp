using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTMP;
    [SerializeField] private TextMeshProUGUI bestscoreTMP;

    int lastscore;
    int bestscore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bestscoreTMP.color = Color.white;
        GameManager.OnGameFinished += UpdateScores;
        GameManager.OnBestScore += GotBestScore;
    }

    public void UpdateScores()
    {
        Debug.Log("Score updated on gameoverui");
        lastscore = PlayerPrefs.GetInt("lastscore");
        scoreTMP.text = "SCORE: " + lastscore.ToString();

        bestscore = PlayerPrefs.GetInt("bestscore");
        bestscoreTMP.text = "BEST SCORE: " + bestscore.ToString();

    }
    void GotBestScore()
    {
        bestscoreTMP.color = Color.green;
    }
}
