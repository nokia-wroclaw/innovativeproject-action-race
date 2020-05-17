using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DayNightSystem : MonoBehaviour
{
    [SerializeField] Image fadeImage;

    void Start()
    {
        StartCoroutine(ChangeTimeOfDay());
    }

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
