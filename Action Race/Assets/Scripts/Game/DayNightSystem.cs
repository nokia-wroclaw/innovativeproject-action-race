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

    public bool IsNight { get; private set; }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object value;
        if (propertiesThatChanged.TryGetValue(RoomProperty.GameState, out value))
        {
            if ((State)value == State.Play)
            {
                background.sprite = dayBackground;
                IsNight = false;
            }
        }
    }

    public IEnumerator ChangeTimeOfDay()
    {
        yield return FadeIn();

        background.sprite = nightBackground;
        IsNight = true;

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
