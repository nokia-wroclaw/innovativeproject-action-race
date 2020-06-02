using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Toggle volumeToggle;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Text jumpKeyText;
    [SerializeField] Text programAntennaKeyText;
    [SerializeField] Text kickKeyText;

    CanvasGroup canvasGroup;

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

    public KeyCode JumpKey
    {
        set { jumpKeyText.text = value.ToString(); }
    }

    public KeyCode ProgramAntennaKey
    {
        set { programAntennaKeyText.text = value.ToString(); }
    }

    public KeyCode KickKey
    {
        set { kickKeyText.text = value.ToString(); }
    }

    public bool IsActive
    {
        get { return canvasGroup.alpha == 1f; }
        set
        {
            if (value)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
            }
            else
            {
                canvasGroup.alpha = 0f;
                canvasGroup.blocksRaycasts = false;
            }
        }
    }

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Toggle()
    {
        IsActive = !IsActive;
    }
}
