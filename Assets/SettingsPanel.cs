using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musiclevel");
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfxlevel");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void VolumeSliderUpdate()
    {
        AudioManager.Instance.SetVolume(AudioManager.AudioType.music, musicVolumeSlider.value);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musiclevel");
        Debug.Log("Music volume set at UIM: " + musicVolumeSlider.value.ToString());
        AudioManager.Instance.SetVolume(AudioManager.AudioType.sfx, sfxVolumeSlider.value);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfxlevel");


    }
}
