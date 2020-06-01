using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;

public class TimeOfDayController : MonoBehaviourPunCallbacks
{
    [Header("Properties")]
    [SerializeField] Sprite dayBackground;
    [SerializeField] Sprite nightBackground;

    [Header("References")]
    [SerializeField] SpriteRenderer background;
    [SerializeField] Image fadeImage;

    public bool IsNight { get; set; }

    void Start()
    {
        object nightValue;
        ExitGames.Client.Photon.Hashtable customRoomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        if (customRoomProperties.TryGetValue(RoomProperty.Night, out nightValue))
        {
            IsNight = (bool)nightValue;
            if (IsNight)
                background.sprite = nightBackground;
            else
                background.sprite = dayBackground;
        }
        else
            background.sprite = dayBackground;
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object nightValue;
        if (propertiesThatChanged.TryGetValue(RoomProperty.Night, out nightValue))
        {
            if (IsNight == (bool)nightValue) return;
            IsNight = (bool)nightValue;

            if (IsNight)
                StartCoroutine(SetNight());
            else
                background.sprite = dayBackground;
        }
    }

    public IEnumerator SetNight()
    {
        yield return FadeIn();

        background.sprite = nightBackground;

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
