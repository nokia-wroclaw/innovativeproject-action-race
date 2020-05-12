using UnityEngine;
using Photon.Pun;

public class QuickPlayController : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject mainMenuPanelGO;
    [SerializeField] GameObject createRoomPanelGO;

    public void QuickPlay()
    {
        Debug.Log("QuickPlay");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        mainMenuPanelGO.SetActive(false);
        createRoomPanelGO.SetActive(true);
    }
}
