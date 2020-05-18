using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;

public class DayNightSystem : MonoBehaviourPunCallbacks
{
    [SerializeField] Image fadeImage;

    void Start()
    {
        StartCoroutine(ChangeTimeOfDay());
    }

    //public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    //{
        
    //}

    IEnumerator ChangeTimeOfDay()
    {
        yield return new WaitForSeconds(5f);

        yield return FadeIn();
        yield return new WaitForSeconds(1f);

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
