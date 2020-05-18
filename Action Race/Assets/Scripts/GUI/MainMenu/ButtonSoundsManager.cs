using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundsManager : MonoBehaviour
{
    public AudioSource audioSource, audioSourceCamera;

    public AudioClip hoverClip;
    public AudioClip clickClip;

    public AudioClip checkVolumeClip;

    public void HoverSound()
    {
        audioSource.PlayOneShot(hoverClip);
    }

    public void ClickSound()
    {
        audioSource.PlayOneShot(clickClip);
    }

    // playes clip in options panel for checking the volume

    public void CheckVolume()
    {
        audioSourceCamera.PlayOneShot(checkVolumeClip);
    }
}
