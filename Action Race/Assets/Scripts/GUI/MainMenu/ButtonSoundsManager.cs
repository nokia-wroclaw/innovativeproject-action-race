using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundsManager : MonoBehaviour
{

    public AudioSource audioSource, audioSourceCamera;

    public AudioClip hoverClip;
    public AudioClip clickClip;

    public AudioClip checkVolumeClip;

    // functions for playing sounds

    public void hoverSound()
    {
        audioSource.PlayOneShot(hoverClip);
    }

    public void clickSound()
    {
        audioSource.PlayOneShot(clickClip);
    }

    // playes clip in options panel for checking the volume

    public void checkVolume()
    {
        audioSourceCamera.PlayOneShot(checkVolumeClip);
    }
}
