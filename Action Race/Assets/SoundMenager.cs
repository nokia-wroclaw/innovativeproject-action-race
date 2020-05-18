using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMenager : MonoBehaviour
{
    public static AudioClip jumpAudio, programAudio;
    static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        jumpAudio = Resources.Load<AudioClip>("jump");
        programAudio = Resources.Load<AudioClip>("program");
    }

    public static void Playsound(string sound)
    {
        switch (sound)
        {
            case "jump":
                audioSource.PlayOneShot(jumpAudio);
                break;
            case "program":
                audioSource.PlayOneShot(programAudio);
                break;

        }
    }
}
