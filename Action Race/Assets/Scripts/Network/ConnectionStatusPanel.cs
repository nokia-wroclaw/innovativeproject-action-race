using UnityEngine;
using UnityEngine.UI;

public class ConnectionStatusPanel : MonoBehaviour
{
    [SerializeField] GameObject connectionStatusPanel;
    [SerializeField] Text messageText;

    [SerializeField] string joinRandomMessage = "Searching the available room...";
    [SerializeField] string joinRandomFailedMessage = "No room available!";
    [SerializeField] string createRoomInfo = "Creating the room ...";
    [SerializeField] string createRoomFailInfo = "Unable to create the room!";
    [SerializeField] string joinRoomInfo = "Joining to the room ...";
    [SerializeField] string joinRoomFailInfo = "Unable to join the room!";

    public void ShowMessage(string msg)
    {
        messageText.text = msg;
        connectionStatusPanel.SetActive(true);
    }

    public string Message
    {
        set 
        {
            messageText.text = value;
        }
    }

    public void SetActive(bool active)
    {
        connectionStatusPanel.SetActive(active);
    }

    public void ShowCreateRoomInfo()
    {
        this.Message = createRoomInfo;
        this.SetActive(true);
    }

    public void ShowCreateRoomFailInfo()
    {
        this.Message = createRoomFailInfo;
        this.SetActive(true);
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
    }
}
