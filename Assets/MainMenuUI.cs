using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public void MuteUnmute()
    {
        AudioManager.Instance.MuteUnmute();
    }
    public void PlayGame()
    {
        GameManager.Instance.LoadScene(1);
        ButtonClickSFX();
    }
    public void Settings()
    {
        GameManager.Instance.LoadScene(3);
        ButtonClickSFX();
    }
    public void MainMenu()
    {
        GameManager.Instance.LoadScene(0);
        ButtonClickSFX();
    }
    public void Credits()
    {
        ButtonClickSFX();
    }
    public void ButtonClickSFX()
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFXClip.click);
    }
}
