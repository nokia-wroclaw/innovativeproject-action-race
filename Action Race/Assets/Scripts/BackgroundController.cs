using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(AudioSource))]
public class BackgroundController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] Sprite dayBackground;
    [SerializeField] Sprite nightBackground;
    [SerializeField] AudioClip dayMusic;
    [SerializeField] AudioClip nightMusic;

    [SerializeField] GameObject nightlight1;
    [SerializeField] GameObject nightlight2;
    [SerializeField] GameObject daylight;

    static BackgroundController _instance;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        daylight.SetActive(true);
        nightlight1.SetActive(false);
        nightlight2.SetActive(false);

        if (_instance != null && _instance != this)
            DestroyImmediate(gameObject);
        else
        {
            _instance = this;
            ObjectExtension.DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    void Start()
    {
        SettingsController settingsController = FindObjectOfType<SettingsController>();
        if(settingsController)
        {
            audioSource.mute = settingsController.Mute;
            audioSource.volume = settingsController.Volume;
        }
    }

    public void ChangeBackground(bool night)
    {
        if (night)
        {
            daylight.SetActive(false);
            //nightlight1.SetActive(true);
            //nightlight2.SetActive(true);
            spriteRenderer.sprite = nightBackground;
        }
        else
        {
            daylight.SetActive(true);
            //nightlight1.SetActive(false);
            //nightlight2.SetActive(false);
            spriteRenderer.sprite = dayBackground;
        }
    }
    public void ChangeMusic(bool night)
    {
        if (night)
            audioSource.clip = nightMusic;
        else
            audioSource.clip = dayMusic;

        audioSource.Play();
    }
}
