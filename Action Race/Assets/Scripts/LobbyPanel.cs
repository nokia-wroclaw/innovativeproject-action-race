using UnityEngine;
using UnityEngine.UI;

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

    public bool IsActive
    {
        get { return canvasGroup.alpha == 1f; }
        set
        {
            if (value)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
            }
            else
            {
                canvasGroup.alpha = 0f;
                canvasGroup.blocksRaycasts = false;
            }
        }
    }

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Toggle()
    {
        IsActive = !IsActive;
    }
}
