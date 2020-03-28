using UnityEngine;
using UnityEngine.UI;

public class ConnectionStatusPanel : MonoBehaviour
{
    [SerializeField] Text messageText;

    public string Message
    {
        set 
        {
            messageText.text = value;
        }
    }
}
