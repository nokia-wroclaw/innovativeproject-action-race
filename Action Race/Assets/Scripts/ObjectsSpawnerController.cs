using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ObjectsSpawnerController : MonoBehaviourPunCallbacks
{
    [Header("References")]
    [SerializeField] Transform[] blueTeamSpawns;
    [SerializeField] Transform[] redTeamSpawns;
    [SerializeField] Transform[] antennaSpawns;
    [SerializeField] Transform[] nokiaSpawns;
    [SerializeField] GameObject viewCamera;

    void Start()
    {
        object gameStateValue;
        ExitGames.Client.Photon.Hashtable customRoomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        if (customRoomProperties.TryGetValue(RoomProperty.GameState, out gameStateValue))
        {
            switch ((State)gameStateValue)
            {
                case State.Stop:
                    StopGame();
                    break;

                case State.Play:
                    StartGame();
                    break;
            }
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object gameStateValue;
        if (propertiesThatChanged.TryGetValue(RoomProperty.GameState, out gameStateValue))
        {
            switch ((State)gameStateValue)
            {
                case State.Stop:
                    StopGame();
                    break;

                case State.Play:
                    StartGame();
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
            object gameStateValue;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(RoomProperty.GameState, out gameStateValue))
            {
                if ((State)gameStateValue != State.Play) return;

                PhotonNetwork.DestroyPlayerObjects(targetPlayer.ActorNumber, false);
                switch ((Team)teamValue)
                {
                    case Team.Blue:
                        SpawnPlayer(blueTeamSpawns);
                        break;

                    case Team.Red:
                        SpawnPlayer(redTeamSpawns);
                        break;

                    default:
                        viewCamera.SetActive(true);
                        break;
                }
            }
        }
    }

    void StopGame()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.DestroyAll();

        viewCamera.SetActive(true);
    }

    void StartGame()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer.ActorNumber, false);

        object teamValue;
        ExitGames.Client.Photon.Hashtable customPlayerProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        if (customPlayerProperties.TryGetValue(PlayerProperty.Team, out teamValue))
        {
            switch ((Team)teamValue)
            {
                case Team.Blue:
                    SpawnPlayer(blueTeamSpawns);
                    break;

                case Team.Red:
                    SpawnPlayer(redTeamSpawns);
                    break;
            }
        }

        foreach (Transform spawn in antennaSpawns)
            PhotonNetwork.InstantiateSceneObject("BasicAntenna", spawn.position, Quaternion.identity);

        foreach (Transform spawn in nokiaSpawns)
            PhotonNetwork.InstantiateSceneObject("Nokia", spawn.position, Quaternion.identity);
    }

    void SpawnPlayer(Transform[] spawns)
    {
        int idSpawn = Random.Range(0, spawns.Length);
        PhotonNetwork.Instantiate("Player", spawns[idSpawn].position, Quaternion.identity);

        viewCamera.SetActive(false);
    }
}
