using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

[RequireComponent(typeof(StatePanel))]
public class StateController : MonoBehaviourPunCallbacks
{
    StatePanel statePanel;

    void Awake()
    {
        statePanel = GetComponent<StatePanel>();
    }

    void Start()
    {
        statePanel.ConfigureAccess(PhotonNetwork.IsMasterClient);
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object gameStateValue;
        if (propertiesThatChanged.TryGetValue(RoomProperty.GameState, out gameStateValue))
            statePanel.ConfigureAccess(PhotonNetwork.IsMasterClient, (State)gameStateValue);
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        if (PhotonNetwork.LocalPlayer != newMasterClient) return;

        object gameStateValue;
        ExitGames.Client.Photon.Hashtable customRoomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        if (customRoomProperties.TryGetValue(RoomProperty.GameState, out gameStateValue))
            statePanel.ConfigureAccess(true, (State)gameStateValue);
        else
            statePanel.ConfigureAccess(true, State.Stop);
    }

    public void StartGame()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        ExitGames.Client.Photon.Hashtable defaultCustomProperties = new ExitGames.Client.Photon.Hashtable();
        defaultCustomProperties.Add(RoomProperty.StartTime, PhotonNetwork.Time);
        defaultCustomProperties.Add(RoomProperty.Night, false);
        defaultCustomProperties.Add(RoomProperty.BlueScore, 0);
        defaultCustomProperties.Add(RoomProperty.RedScore, 0);
        defaultCustomProperties.Add(RoomProperty.GameState, State.Play);
        PhotonNetwork.CurrentRoom.SetCustomProperties(defaultCustomProperties);
    }

    public void StopGame()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        ExitGames.Client.Photon.Hashtable gameStateProperty = new ExitGames.Client.Photon.Hashtable();
        gameStateProperty.Add(RoomProperty.GameState, State.Stop);
        PhotonNetwork.CurrentRoom.SetCustomProperties(gameStateProperty);
    }
}
