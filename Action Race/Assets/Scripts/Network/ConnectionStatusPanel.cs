using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class ConnectionStatusPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Text messageText;

    [Header("Messages")]
    [SerializeField] string createRoomInfo = "Creating the room ...";
    [SerializeField] string createRoomFailInfo = "Unable to create the room!";
    [SerializeField] string joinRoomInfo = "Joining to the room ...";
    [SerializeField] string joinRoomFailInfo = "Unable to join the room!";
    [SerializeField] string passwordWrongInfo = "Entered Password is wrong!";

    CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.gameObject.SetActive(true);
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

            case ConnectionStatus.PasswordWrong:
                Message = passwordWrongInfo;
                break;
        }
    }

    public IEnumerator MessageFadeInOut(ConnectionStatus connectionStatus)
    {
        yield return MessageFadeIn(connectionStatus);
        yield return MessageFadeOut();
    }

    public IEnumerator MessageFadeIn(ConnectionStatus connectionStatus)
    {
        canvasGroup.blocksRaycasts = true;

        ChangeMessage(connectionStatus);

        for (float a = 0; a <= 1; a += 2 * Time.deltaTime)
        {
            canvasGroup.alpha = a;
            yield return null;
        }
    }

    public IEnumerator MessageFadeOut()
    {
        for (float a = 1; a >= 0; a -= Time.deltaTime)
        {
            canvasGroup.alpha = a;
            yield return null;
        }

        canvasGroup.blocksRaycasts = false;
    }
}
