using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameState : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform[] antennasTransforms;
    [SerializeField] GameLobbyPanel glp;

    void Start()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        EndGame();
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object value;
        if (propertiesThatChanged.TryGetValue(RoomProperty.GameState, out value))
        {
            State state = (State)value;
            switch(state)
            {
                case State.End:
                    if (PhotonNetwork.IsMasterClient)
                        PhotonNetwork.DestroyAll();

                    glp.SetActive(true);
                    break;

                case State.Play:
                    if (PhotonNetwork.IsMasterClient)
                        SpawnAntennas();

                    SpawnPlayer();
                    glp.SetActive(false);
                    break;

                case State.Pause:
                    break;
            }

            glp.UpdateGameStateButtons(state);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (!newPlayer.IsLocal) return;

        Debug.Log("HM");
        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
        if((State)hash[RoomProperty.GameState] == State.Play)
        {
            Debug.Log("HM1");
            SpawnPlayer();
        }
    }

    void SpawnAntennas()
    {
        foreach(Transform t in antennasTransforms)
        {
            PhotonNetwork.InstantiateSceneObject("BasicAntenna", t.position, Quaternion.identity);
        }
    }

    void SpawnPlayer()
    {
        PhotonNetwork.Instantiate("Player", Vector3.up * -4f, Quaternion.identity);
    }

    public void StartGame()
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.GameState, State.Play);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    public void PauseGame()
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.GameState, State.Pause);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    public void EndGame()
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.GameState, State.End);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }
}
