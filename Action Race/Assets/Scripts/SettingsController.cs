using UnityEngine;

[RequireComponent(typeof(SettingsPanel), typeof(CanvasGroup))]
public class SettingsController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float defaultVolume = 0.5f;

    [Header("References")]
    [SerializeField] AudioSource audioSource;

    SettingsPanel settingsPanel;

    public bool Mute
    {
        get
        {
            if (PlayerPrefs.HasKey("MusicMute"))
                return PlayerPrefs.GetInt("MusicMute") != 0;
            else
                return false;
        }

        set
        {
            PlayerPrefs.SetInt("MusicMute", value ? 1 : 0);
            audioSource.mute = value;
        }
    }

    public float Volume
    {
        get
        {
            if (PlayerPrefs.HasKey("MusicVolume"))
                return PlayerPrefs.GetFloat("MusicVolume");
            else
                return defaultVolume;
        }

        set
        {
            PlayerPrefs.SetFloat("MusicVolume", value);
            audioSource.volume = value;
        }
    }

    void Awake()
    {
        settingsPanel = GetComponent<SettingsPanel>();
    }

    void Start()
    {
        settingsPanel.Mute = Mute;
        settingsPanel.Volume = Volume;

        settingsPanel.IsActive = false;
    }
}
