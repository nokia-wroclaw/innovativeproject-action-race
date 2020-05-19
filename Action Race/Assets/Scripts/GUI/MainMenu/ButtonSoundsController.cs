using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSoundsController : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip hoverClip;
    public AudioClip clickClip;

    public AudioClip checkVolumeClip;

    public void HoverSound(BaseEventData baseEventData)
    {
        GameObject buttonGO;
        if (buttonGO = baseEventData.selectedObject)
        {
            Button button = buttonGO.GetComponent<Button>();
            if (!button || !button.interactable)
                return;
        }
        else
            return;

        audioSource.PlayOneShot(hoverClip);
    }

    public void ClickSound(BaseEventData baseEventData)
    {
        GameObject buttonGO;
        if (buttonGO = baseEventData.selectedObject)
        {
            Button button = buttonGO.GetComponent<Button>();
            if (!button || !button.interactable)
                return;
        }
        else
            return;

        audioSource.PlayOneShot(clickClip);
    }

    public void CheckVolume()
    {
        audioSource.PlayOneShot(checkVolumeClip);
    }
}
