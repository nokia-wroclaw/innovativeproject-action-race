using UnityEngine;
using Photon.Pun;

public class QuickPlayManager : MonoBehaviourPunCallbacks
{
    [SerializeField] UIManager uiManager;

    public void QuickPlay()
    {
        Debug.Log("QuickPlay");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No room to join, create your own");

        uiManager.switchFrom_QuickPlayPanel_to_CreateRoomPanel();
    }
}
