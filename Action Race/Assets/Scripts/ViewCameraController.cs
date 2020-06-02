using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(Camera))]
public class ViewCameraController : MonoBehaviourPunCallbacks
{
    new Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object gameStateValue;
        if (propertiesThatChanged.TryGetValue(RoomProperty.GameState, out gameStateValue))
        {
            switch ((State)gameStateValue)
            {
                case State.Stop:
                    camera.enabled = true;
                    break;

                case State.Play:
                    object teamValue;
                    if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(PlayerProperty.Team, out teamValue))
                        if ((Team)teamValue != Team.None)
                            camera.enabled = false;

                    break;
            }
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (!targetPlayer.IsLocal) return;

        object teamValue;
        if (changedProps.TryGetValue(PlayerProperty.Team, out teamValue))
        {
            if ((Team)teamValue == Team.None)
            {
                camera.enabled = true;
                return;
            }

            object gameStateValue;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(RoomProperty.GameState, out gameStateValue))
            {
                switch((State)gameStateValue)
                {
                    case State.Play:
                        camera.enabled = false;
                        break;
                }
            }
        }
    }
}
