using UnityEngine;
using Photon.Pun;

public class GameState : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform[] antennasTransforms;
    [SerializeField] GameLobbyPanel glp;

    GameScore gs;
    GameTime gt;

    void Start()
    {
        gs = FindObjectOfType<GameScore>();
        gt = FindObjectOfType<GameTime>();

        if(PhotonNetwork.IsMasterClient)
        {
            EndGame();
        }
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
                    PhotonNetwork.DestroyAll();
                    glp.ToggleLobbyPanel();
                    break;

                case State.Play:
                    SpawnAntennas();
                    SpawnPlayer();

                    if (PhotonNetwork.IsMasterClient)
                    {
                        gs.ResetScore();
                        gt.ResetTime();
                    }

                    glp.ToggleLobbyPanel();
                    break;

                case State.Pause:
                    break;
            }

            glp.UpdateGameStateButtons(state);
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
