using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class GameLobbyPanel : MonoBehaviourPunCallbacks
{
    [Header("Properties")]
    [SerializeField] int startTimeLimitID = 3;
    [SerializeField] int startScoreLimitID = 3;

    [Header("References")]
    [SerializeField] Text currentPlayersCountText;
    [SerializeField] Text maxPlayersCountText;
    [SerializeField] Text roomNameText;

    [SerializeField] RectTransform blueTeamPanel;
    [SerializeField] RectTransform noTeamPanel;
    [SerializeField] RectTransform redTeamPanel;
    [SerializeField] GameObject playerTemplate;

    [SerializeField] Dropdown timeLimitDropdown;
    [SerializeField] Dropdown scoreLimitDropdown;
    [SerializeField] Text timeLimitText;
    [SerializeField] Text scoreLimitText;

    [SerializeField] GameObject gameLobbyPanel;

    [SerializeField] GameObject startGameButton;
    [SerializeField] GameObject stopGameButton;
    [SerializeField] GameObject pauseGameButton;

    [SerializeField] GameObject moveBlueToSpec;
    [SerializeField] GameObject moveRedToSpec;

    GameLobbyController glc;
    Dictionary<Player, GameObject> playersTemplates = new Dictionary<Player, GameObject>();

    void Start()
    {
        glc = FindObjectOfType<GameLobbyController>();

        SynchronizeCustomProperties();

        // PLAYERS COUNT SYNCHRO
        UpdateCurrentPlayersCountText();
        maxPlayersCountText.text = PhotonNetwork.CurrentRoom.MaxPlayers.ToString();
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        SynchronizeTeams();

        ConfigurePanel();
        SetActive(true);


        //TEMPORARY OFF
        pauseGameButton.SetActive(false);
        moveBlueToSpec.SetActive(false);
        moveRedToSpec.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object value;
        
        if (propertiesThatChanged.TryGetValue(RoomProperty.TimeLimit, out value))
        {
            timeLimitDropdown.value = ((int)value / 60) - 1;
            timeLimitText.text = timeLimitDropdown.options[timeLimitDropdown.value].text;
        }

        if (propertiesThatChanged.TryGetValue(RoomProperty.ScoreLimit, out value))
        {
            scoreLimitDropdown.value = (int)value - 1;
            scoreLimitText.text = scoreLimitDropdown.options[scoreLimitDropdown.value].text;
        }

        if (propertiesThatChanged.TryGetValue(RoomProperty.GameState, out value))
            if (PhotonNetwork.IsMasterClient)
                ConfigureMasterPanel((State)value);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        object value;
        if (changedProps.TryGetValue(PlayerProperty.Team, out value))
            SynchronizePlayerTemplate(targetPlayer, (Team)value);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
            object value;

            if (hash.TryGetValue(RoomProperty.GameState, out value))
                ConfigureMasterPanel((State)value);
            else
                ConfigureMasterPanel(State.NotStarted);
        }

        GameObject go;
        if (playersTemplates.TryGetValue(newMasterClient, out go))
            go.GetComponent<PlayerTemplate>().SetStatus("HOST");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdateCurrentPlayersCountText();
        SynchronizePlayerTemplate(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateCurrentPlayersCountText();
        RemovePlayerTemplate(otherPlayer);

        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(PlayerProperty.Team, Team.None);
        otherPlayer.SetCustomProperties(hash);
    }

    void SynchronizeCustomProperties()
    {
        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
        object value;

        // TIME LIMIT SYNCHRO
        if (hash.TryGetValue(RoomProperty.TimeLimit, out value))
            timeLimitDropdown.value = ((int)value / 60) - 1;
        else
            timeLimitDropdown.value = startTimeLimitID;
        timeLimitText.text = timeLimitDropdown.options[timeLimitDropdown.value].text;

        // SCORE LIMIT SYNCHRO
        if (hash.TryGetValue(RoomProperty.ScoreLimit, out value))
            scoreLimitDropdown.value = (int)value - 1;
        else
            scoreLimitDropdown.value = startScoreLimitID;
        scoreLimitText.text = scoreLimitDropdown.options[scoreLimitDropdown.value].text;
    }

    void SynchronizeTeams()
    {
        foreach (Transform child in blueTeamPanel)
            Destroy(child.gameObject);

        foreach (Transform child in redTeamPanel)
            Destroy(child.gameObject);

        foreach (Transform child in noTeamPanel)
            Destroy(child.gameObject);

        foreach (var p in PhotonNetwork.CurrentRoom.Players)
            SynchronizePlayerTemplate(p.Value);
    }

    void ConfigurePanel()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
            object value;

            if (hash.TryGetValue(RoomProperty.GameState, out value))
                ConfigureMasterPanel((State)value);
            else
                ConfigureMasterPanel(State.NotStarted);
        }
        else
            ConfigurePlayerPanel();
    }

    void ConfigureMasterPanel(State state)
    {
        switch (state)
        {
            case State.NotStarted:
                startGameButton.SetActive(true);
                stopGameButton.SetActive(false);
                //pauseGameButton.SetActive(false);

                timeLimitText.gameObject.SetActive(false);
                scoreLimitText.gameObject.SetActive(false);
                timeLimitDropdown.gameObject.SetActive(true);
                scoreLimitDropdown.gameObject.SetActive(true);

                //moveBlueToSpec.SetActive(true);
                //moveRedToSpec.SetActive(true);
                break;

            case State.Play:
                startGameButton.SetActive(false);
                stopGameButton.SetActive(true);
                //pauseGameButton.SetActive(true);

                timeLimitDropdown.gameObject.SetActive(false);
                scoreLimitDropdown.gameObject.SetActive(false);
                timeLimitText.gameObject.SetActive(true);
                scoreLimitText.gameObject.SetActive(true);

               // moveBlueToSpec.SetActive(false);
                //moveRedToSpec.SetActive(false);
                break;
        }
    }

    void ConfigurePlayerPanel()
    {
        startGameButton.SetActive(false);
        stopGameButton.SetActive(false);
        //pauseGameButton.SetActive(false);

        timeLimitDropdown.gameObject.SetActive(false);
        scoreLimitDropdown.gameObject.SetActive(false);
        timeLimitText.gameObject.SetActive(true);
        scoreLimitText.gameObject.SetActive(true);

        //moveBlueToSpec.SetActive(false);
        //moveRedToSpec.SetActive(false);
    }

    void SynchronizePlayerTemplate(Player player, Team team = Team.None)
    {
        GameObject go;

        if (playersTemplates.TryGetValue(player, out go))
        {
            switch (team)
            {
                case Team.Blue:
                    go.transform.SetParent(blueTeamPanel);
                    break;

                case Team.Red:
                    go.transform.SetParent(redTeamPanel);
                    break;

                default:
                    go.transform.SetParent(noTeamPanel);
                    break;
            }
        }
        else
        {
            ExitGames.Client.Photon.Hashtable hash = player.CustomProperties;
            object value;

            if (hash.TryGetValue(PlayerProperty.Team, out value))
            {
                switch ((Team)value)
                {
                    case Team.Blue:
                        go = Instantiate(playerTemplate, blueTeamPanel);
                        break;

                    case Team.Red:
                        go = Instantiate(playerTemplate, redTeamPanel);
                        break;

                    default:
                        go = Instantiate(playerTemplate, noTeamPanel);
                        break;
                }
            }
            else
                go = Instantiate(playerTemplate, noTeamPanel);

            PlayerTemplate pt = go.GetComponent<PlayerTemplate>();
            pt.SetUpTemplate(player);

            if (player.IsMasterClient)
                pt.SetStatus("HOST");
            else if (player.IsLocal)
                pt.SetStatus("ME");

            playersTemplates.Add(player, go);
        }
    }

    void RemovePlayerTemplate(Player player)
    {
        GameObject go;

        if (playersTemplates.TryGetValue(player, out go))
        {
            Destroy(go);
            playersTemplates.Remove(player);
        }
    }

    public void UpdateTimeLimitDropdown()
    {
        int time = (timeLimitDropdown.value + 1) * 60;
        glc.ChangeTimeLimit(time);
    }

    public void UpdateScoreLimitDropdown()
    {
        int score = scoreLimitDropdown.value + 1;
        glc.ChangeScoreLimit(score);
    }

    void UpdateCurrentPlayersCountText()
    {
        currentPlayersCountText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
    }

    void Toggle()
    {
        gameLobbyPanel.SetActive(!gameLobbyPanel.activeInHierarchy);
    }

    public void SetActive(bool active)
    {
        gameLobbyPanel.SetActive(active);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void ChangeGameState(int state)
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(RoomProperty.GameState, (State)state);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    /*public void MoveToSpec(int team)
    {
        foreach(var p in playersTemplates)
        {
            object value;
            if(p.Key.CustomProperties.TryGetValue(PlayerProperty.Team, out value))
            {
                SynchronizePlayerTemplate
            }
        }
    }*/
}
