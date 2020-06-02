using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;

public class TimeOfDayController : MonoBehaviourPunCallbacks
{
    [Header("References")]
    [SerializeField] Image fadeImage;

    BackgroundController backgroundController;

    public bool IsNight { get; set; }

    void Awake()
    {
        backgroundController = FindObjectOfType<BackgroundController>();
    }

    void Start()
    {
        object nightValue;
        ExitGames.Client.Photon.Hashtable customRoomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        if (customRoomProperties.TryGetValue(RoomProperty.Night, out nightValue))
        {
            IsNight = (bool)nightValue;
            backgroundController.ChangeBackground(IsNight);
            backgroundController.ChangeMusic(IsNight);
        }
        else
        {
            backgroundController.ChangeBackground(false);
            backgroundController.ChangeMusic(false);
        }
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
            {
                backgroundController.ChangeBackground(false);
                backgroundController.ChangeMusic(false);
            }
        }
    }

    public IEnumerator SetNight()
    {
        yield return FadeIn();

        backgroundController.ChangeBackground(true);
        backgroundController.ChangeMusic(true);

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
