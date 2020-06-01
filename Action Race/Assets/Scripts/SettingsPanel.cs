using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Toggle volumeToggle;
    [SerializeField] Slider volumeSlider;

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
