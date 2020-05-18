using UnityEngine;
using Photon.Pun;

public class PasswordController : MonoBehaviour
{
    [Header("Custom Scripts References")]
    [SerializeField] ConnectionStatusPanel connectionStatusPanel;
    [SerializeField] PasswordPanel passwordPanel;

    public void JoinRoom()
    {
        if (passwordPanel.Password.Trim().Equals(passwordPanel.PasswordExpected))
        {
            StartCoroutine(connectionStatusPanel.MessageFadeIn(ConnectionStatus.Join));
            PhotonNetwork.JoinRoom(passwordPanel.RoomName);
        }
        else
            StartCoroutine(connectionStatusPanel.MessageFadeInOut(ConnectionStatus.PasswordWrong));
    }
}
