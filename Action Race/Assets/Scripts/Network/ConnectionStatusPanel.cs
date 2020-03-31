using UnityEngine;
using UnityEngine.UI;

public class ConnectionStatusPanel : MonoBehaviour
{
    [SerializeField] GameObject connectionStatusPanel;
    [SerializeField] Text messageText;

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
        Debug.Log("XD");
    }
}
