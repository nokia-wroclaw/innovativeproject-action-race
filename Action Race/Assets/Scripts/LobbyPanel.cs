using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class LobbyPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Text playersText;
    [SerializeField] Text maxPlayersText;
    [SerializeField] Text roomNameText;

    CanvasGroup canvasGroup;

    public int Players { set { playersText.text = value.ToString(); } }
    public int MaxPlayers { set { maxPlayersText.text = value.ToString(); } }
    public string RoomName { set { roomNameText.text = value; } }

    public bool IsActive { get { return canvasGroup.alpha == 1f; } }

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Toggle()
    {
        if(IsActive)
            canvasGroup.alpha = 0f;
        else
            canvasGroup.alpha = 1f;
    }
}
