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

    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
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
                SpawnPlayer((Team)teamValue);
            }
        }
    }

    void StopGame()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.DestroyAll();
    }

    void StartGame()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer.ActorNumber, false);

        object teamValue;
        ExitGames.Client.Photon.Hashtable customPlayerProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        if (customPlayerProperties.TryGetValue(PlayerProperty.Team, out teamValue))
            SpawnPlayer((Team)teamValue);

        foreach (Transform spawn in antennaSpawns)
            PhotonNetwork.InstantiateSceneObject("BasicAntenna", spawn.position, Quaternion.identity);

        foreach (Transform spawn in nokiaSpawns)
            PhotonNetwork.InstantiateSceneObject("Nokia", spawn.position, Quaternion.identity);
    }

    void SpawnPlayer(Team team)
    {
        switch(team)
        {
            case Team.Blue:
                int idBlueSpawn = Random.Range(0, blueTeamSpawns.Length);
                int genderBlue = Random.Range(0, 2);
                PhotonNetwork.Instantiate(genderBlue == 0 ? "Player Blue Female" : "Player Blue Male", blueTeamSpawns[idBlueSpawn].position, Quaternion.identity);
                break;

            case Team.Red:
                int idRedSpawn = Random.Range(0, redTeamSpawns.Length);
                int genderRed = Random.Range(0, 2);
                PhotonNetwork.Instantiate(genderRed == 0 ? "Player Red Female" : "Player Red Male", redTeamSpawns[idRedSpawn].position, Quaternion.identity);
                break;
        }
    }
}
