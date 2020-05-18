using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConnectionStatusPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CanvasGroup connectionStatusPanelCG;
    [SerializeField] Text messageText;

    [Header("Messages")]
    [SerializeField] string createRoomInfo = "Creating the room ...";
    [SerializeField] string createRoomFailInfo = "Unable to create the room!";
    [SerializeField] string joinRoomInfo = "Joining to the room ...";
    [SerializeField] string joinRoomFailInfo = "Unable to join the room!";

    void Start()
    {
        connectionStatusPanelCG.alpha = 0;
        connectionStatusPanelCG.blocksRaycasts = false;
        connectionStatusPanelCG.gameObject.SetActive(true);
    }

    public string Message
    {
        set { messageText.text = value; }
    }

    public void ChangeMessage(ConnectionStatus connectionStatus)
    {
        switch (connectionStatus)
        {
            case ConnectionStatus.Create:
                Message = createRoomInfo;
                break;

            case ConnectionStatus.CreateFail:
                Message = createRoomFailInfo;
                break;

            case ConnectionStatus.Join:
                Message = joinRoomInfo;
                break;

            case ConnectionStatus.JoinFail:
                Message = joinRoomFailInfo;
                break;
        }
    }

    public IEnumerator MessageFadeIn(ConnectionStatus connectionStatus)
    {
        connectionStatusPanelCG.blocksRaycasts = true;

        ChangeMessage(connectionStatus);

        for (float a = 0; a <= 1; a += 2 * Time.deltaTime)
        {
            connectionStatusPanelCG.alpha = a;
            yield return null;
        }
    }

    public IEnumerator MessageFadeOut()
    {
        connectionStatusPanelCG.blocksRaycasts = false;

        for (float a = 1; a >= 0; a -= Time.deltaTime)
        {
            connectionStatusPanelCG.alpha = a;
            yield return null;
        }
    }

    /*public void ShowMessage(string msg)
    {
        messageText.text = msg;
        connectionStatusPanel.SetActive(true);
    }

    public void SetActive(bool active)
    {
        connectionStatusPanel.SetActive(active);
    }

    public void ShowCreateRoomInfo()
    {
        Message = createRoomInfo;
        SetActive(true);
    }

    public void ShowCreateRoomFailInfo()
    {
        Message = createRoomFailInfo;
        SetActive(true);
    }

    public void ShowJoinRoomInfo()
    {
        this.Message = joinRoomInfo;
        this.SetActive(true);
    }

    public void ShowJoinRoomFailInfo()
    {
        this.Message = joinRoomFailInfo;
        this.SetActive(true);
    }*/
}
