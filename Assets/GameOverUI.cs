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

    }

    // Update is called once per frame
    void Update()
    {
        lastscore = PlayerPrefs.GetInt("lastscore");
        scoreTMP.text = "SCORE: " + lastscore.ToString();

        bestscore = PlayerPrefs.GetInt("bestscore");
        bestscoreTMP.text = "BEST SCORE: " + bestscore.ToString();

        if (lastscore >= bestscore)
        {
            bestscoreTMP.color = Color.green;
        }

    }
}
