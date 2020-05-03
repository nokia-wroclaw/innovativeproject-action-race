using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameState : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform[] antennasWaypoints;
    [SerializeField] GameObject viewCamera;

    GameLobbyPanel glp;

    void Start()
    {
        glp = FindObjectOfType<GameLobbyPanel>();

        if (PhotonNetwork.IsMasterClient)
        {
            ResetState();
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object value;
        if (propertiesThatChanged.TryGetValue(RoomProperty.GameState, out value))
        {
            State state = (State)value;
            switch (state)
            {
                case State.NotStarted:
                    StopGame();
                    break;

                case State.Play:
                    SetGame();
                    break;
            }
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }

    public void ChangeGameState(int state)
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.GameState, (State)state);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    void StopGame()
    {
        viewCamera.SetActive(true);

        PhotonNetwork.DestroyAll();
    }

    void SetGame()
    {
        viewCamera.SetActive(false);

        PhotonNetwork.Instantiate("Player", Vector3.up * -3f, Quaternion.identity);
        foreach (Transform waypoint in antennasWaypoints)
        {
            PhotonNetwork.InstantiateSceneObject("BasicAntenna", waypoint.position, Quaternion.identity);
        }

        glp.SetActive(false);
    }

    void ResetState()
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.GameState, State.NotStarted);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    /*[SerializeField] Transform[] antennasTransforms;
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
    }*/
}
