using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    bool isMuted = false;
    [Header("Music")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip bgmLoop;
    [SerializeField] private float musicLevel;

    [Header("SFX")]
    [SerializeField] private AudioSource sFXSource;
    [SerializeField] private float sfxLevel;
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip diedSFX;
    [SerializeField] private AudioClip buttonClickSFX;
    [SerializeField] private AudioClip bestScoreSFX;

    public static AudioManager Instance;

    public enum SFXClip
    {
        jump,
        dead,
        click,
        bestscore,
    }
    public enum AudioType
    {
        music,
        sfx,
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this.gameObject);
        }


    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Default Settings
        if (!PlayerPrefs.HasKey("isMuted"))
        {
            PlayerPrefs.SetInt("isMuted", 0);
        }
        if (!PlayerPrefs.HasKey("musiclevel"))
        {
            PlayerPrefs.SetFloat("musiclevel", musicLevel);
        }
        if (!PlayerPrefs.HasKey("sfxlevel"))
        {
            PlayerPrefs.SetFloat("sfxlevel", sfxLevel);
        }
        isMuted = PlayerPrefs.GetInt("isMuted") > 0;
        musicLevel = PlayerPrefs.GetFloat("musiclevel");
        musicSource.volume = musicLevel;
        musicSource.clip = bgmLoop;
        musicSource.Play();
        sfxLevel = PlayerPrefs.GetFloat("sfxlevel");
        sFXSource.volume = sfxLevel;


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Mute()
    {
        if (isMuted)
        {
            return;
        }
        SetVolume(AudioType.music, 0f);
        SetVolume(AudioType.sfx, 0f);
        PlayerPrefs.SetInt("isMuted", 1);
        PlayerPrefs.Save();
        isMuted = true;
    }
    public void Unmute()
    {
        if (!isMuted)
        {
            return;
        }
        SetVolume(AudioType.music, PlayerPrefs.GetFloat("musiclevel"));
        SetVolume(AudioType.sfx, PlayerPrefs.GetFloat("sfxlevel"));
        PlayerPrefs.SetInt("isMuted", 0);
        PlayerPrefs.Save();
        isMuted = false;
    }
    public void SetVolume(AudioType type, float volume)
    {
        switch (type)
        {
            case AudioType.music:
                PlayerPrefs.SetFloat("musiclevel", volume);
                PlayerPrefs.Save();
                musicLevel = PlayerPrefs.GetFloat("musiclevel");
                musicSource.volume = musicLevel;
                break;
            case AudioType.sfx:
                PlayerPrefs.SetFloat("sfxlevel", volume);
                PlayerPrefs.Save();
                sfxLevel = PlayerPrefs.GetFloat("sfxlevel");
                sFXSource.volume = sfxLevel;
                break;
            default:
                break;
        }

    }
    public void PlaySFX(SFXClip sFXClip)
    {
        switch (sFXClip)
        {
            case SFXClip.jump:

                sFXSource.PlayOneShot(jumpSFX);
                break;
            case SFXClip.dead:
                sFXSource.PlayOneShot(diedSFX);
                break;
            case SFXClip.click:
                sFXSource.PlayOneShot(buttonClickSFX);
                break;
            case SFXClip.bestscore:
                sFXSource.PlayOneShot(bestScoreSFX);
                break;

            default:
                break;
        }
    }
}
