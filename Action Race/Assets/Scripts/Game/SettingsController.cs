using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float defaultVolume = 0.75f;

    [Header("Custom Scripts References")]
    [SerializeField] SettingsPanel settingsPanel;

    [Header("References")]
    [SerializeField] AudioSource audioSource;

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

    void Start()
    {
        settingsPanel.Mute = Mute;
        settingsPanel.Volume = Volume;
    }

    public void MuteMusic(bool mute)
    {
        Mute = mute;
    }

    public void ChangeVolume(float volume)
    {
        Volume = volume;
    }
}
