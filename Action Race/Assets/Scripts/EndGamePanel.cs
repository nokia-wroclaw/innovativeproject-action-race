using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGamePanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject blueTeamText;
    [SerializeField] GameObject redTeamText;
    [SerializeField] GameObject wonText;
    [SerializeField] GameObject drawText;

    CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public IEnumerator ShowEndPanel(Team winner)
    {
        switch(winner)
        {
            case Team.Blue:
                blueTeamText.SetActive(true);
                redTeamText.SetActive(false);
                wonText.SetActive(true);
                drawText.SetActive(false);
                break;

            case Team.Red:
                blueTeamText.SetActive(false);
                redTeamText.SetActive(true);
                wonText.SetActive(true);
                drawText.SetActive(false);
                break;

            default:
                blueTeamText.SetActive(false);
                redTeamText.SetActive(false);
                wonText.SetActive(false);
                drawText.SetActive(true);
                break;
        }

        for(float a = 0; a <= 1; a += Time.deltaTime)
        {
            canvasGroup.alpha = a;
            yield return null;
        }
    }

    public IEnumerator HideEndPanel()
    {
        for (float a = 1; a >= 0; a -= Time.deltaTime)
        {
            canvasGroup.alpha = a;
            yield return null;
        }
    }
}
