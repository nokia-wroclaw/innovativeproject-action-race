using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Toggle volumeToggle;
    [SerializeField] Slider volumeSlider;

    public bool Mute
    {
        get { return volumeToggle.isOn; }
        set { volumeToggle.isOn = value; }
    }

    public float Volume
    {
        get { return volumeSlider.value; }
        set { volumeSlider.value = value; }
    }
}
