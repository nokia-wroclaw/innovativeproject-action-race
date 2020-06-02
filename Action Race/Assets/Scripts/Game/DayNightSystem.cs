using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;

public class DayNightSystem : MonoBehaviourPunCallbacks
{
    [Header("References")]
    [SerializeField] Sprite dayBackground;
    [SerializeField] Sprite nightBackground;

    [Header("References")]
    [SerializeField] Image fadeImage;
    [SerializeField] SpriteRenderer background;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip night;

    public bool IsNight { get; set; }

    void Start()
    {
        ExitGames.Client.Photon.Hashtable customRoomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        UpdateNight(customRoomProperties);
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object value;
        if (propertiesThatChanged.TryGetValue(RoomProperty.GameState, out value))
        {
            if ((State)value == State.Play)
            {
                IsNight = false;
                background.sprite = dayBackground;
            }
        }
    }

    void UpdateNight(ExitGames.Client.Photon.Hashtable properties)
    {
        object value;
        if (properties.TryGetValue(RoomProperty.Night, out value))
        {
            IsNight = (bool)value;
            if(IsNight)
                background.sprite = nightBackground;
            else
                background.sprite = dayBackground;
        }
        else
            background.sprite = dayBackground;
    }

    public IEnumerator ChangeTimeOfDay()
    {
        audioSource.Stop();
        yield return FadeIn();

        background.sprite = nightBackground;
        IsNight = true;
        audioSource.clip = night;
        audioSource.Play();

        yield return new WaitForSeconds(0.1f);
        yield return FadeOut();
    }

    IEnumerator FadeIn()
    {
        for (float a = 0; a <= 1; a += Time.deltaTime)
        {
            fadeImage.color = new Color(0, 0, 0, a);
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        for (float a = 1; a >= 0; a -= Time.deltaTime)
        {
            fadeImage.color = new Color(0, 0, 0, a);
            yield return null;
        }
    }
}
