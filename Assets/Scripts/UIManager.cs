using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTMP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WriteScore();
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
    public void ButtonClickSFX()
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFXClip.click);
    }
}
