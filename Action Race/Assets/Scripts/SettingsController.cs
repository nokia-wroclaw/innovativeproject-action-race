using UnityEngine;

[RequireComponent(typeof(SettingsPanel), typeof(CanvasGroup))]
public class SettingsController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] bool defaultMute = false;
    [SerializeField] float defaultVolume = 0.25f;
    [SerializeField] KeyCode defaultJumpKey = KeyCode.Space;
    [SerializeField] KeyCode defaultProgramAntennaKey = KeyCode.E;
    [SerializeField] KeyCode defaultKickKey = KeyCode.R;

    SettingsPanel settingsPanel;

    public bool Mute
    {
        get
        {
            if (PlayerPrefs.HasKey(SettingProperty.MusicMute))
                return PlayerPrefs.GetInt(SettingProperty.MusicMute) != 0;
            else
                return defaultMute;
        }

        set
        {
            PlayerPrefs.SetInt(SettingProperty.MusicMute, value ? 1 : 0);
            foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>())
                audioSource.mute = value;
        }
    }

    public float Volume
    {
        get
        {
            if (PlayerPrefs.HasKey(SettingProperty.MusicVolume))
                return PlayerPrefs.GetFloat(SettingProperty.MusicVolume);
            else
                return defaultVolume;
        }

        set
        {
            PlayerPrefs.SetFloat(SettingProperty.MusicVolume, value);
            foreach(AudioSource audioSource in FindObjectsOfType<AudioSource>())
                audioSource.volume = value;
        }
    }

    public KeyCode JumpKey
    {
        get
        {
            if (PlayerPrefs.HasKey(SettingProperty.JumpKey))
                return (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(SettingProperty.JumpKey));
            else
                return defaultJumpKey;
        }

        set
        {
            PlayerPrefs.SetString(SettingProperty.JumpKey, value.ToString());
        }
    }

    public KeyCode ProgramAntennaKey
    {
        get
        {
            if (PlayerPrefs.HasKey(SettingProperty.ProgramAntennaKey))
                return (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(SettingProperty.ProgramAntennaKey));
            else
                return defaultProgramAntennaKey;
        }

        set
        {
            PlayerPrefs.SetString(SettingProperty.ProgramAntennaKey, value.ToString());
        }
    }

    public KeyCode KickKey
    {
        get
        {
            if (PlayerPrefs.HasKey(SettingProperty.KickKey))
                return (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(SettingProperty.KickKey));
            else
                return defaultKickKey;
        }

        set
        {
            PlayerPrefs.SetString(SettingProperty.KickKey, value.ToString());
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

        settingsPanel.JumpKey = JumpKey;
        settingsPanel.ProgramAntennaKey = ProgramAntennaKey;
        settingsPanel.KickKey = KickKey;

        settingsPanel.IsActive = false;
    }
}
