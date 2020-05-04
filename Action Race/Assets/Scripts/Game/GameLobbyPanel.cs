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

    GameLobby gl;
    Dictionary<Player, GameObject> playersTemplates = new Dictionary<Player, GameObject>();

    void Start()
    {
        gl = FindObjectOfType<GameLobby>();

        timeLimitDropdown.value = startTimeLimitID;
        UpdateTimeLimitDropdown();

        scoreLimitDropdown.value = startScoreLimitID;
        UpdateScoreLimitDropdown();

        UpdateCurrentPlayersCountText();
        maxPlayersCountText.text = PhotonNetwork.CurrentRoom.MaxPlayers.ToString();
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        ConfigureTeamPanel();

        if (PhotonNetwork.IsMasterClient)
            ConfigureMasterLobbyPanel(State.NotStarted);
        else
            ConfigurePlayerLobbyPanel();

        SetActive(true);
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
        if (propertiesThatChanged.TryGetValue(RoomProperty.GameState, out value))
            if(PhotonNetwork.IsMasterClient)
                ConfigureMasterLobbyPanel((State)value);

        if (propertiesThatChanged.TryGetValue(RoomProperty.TimeLimit, out value))
            timeLimitText.text = timeLimitDropdown.options[(int)((int)value / 60) - 1].text;

        if (propertiesThatChanged.TryGetValue(RoomProperty.ScoreLimit, out value))
            scoreLimitText.text = scoreLimitDropdown.options[(int)value - 1].text;
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        object value;
        if (changedProps.TryGetValue(PlayerProperty.Team, out value))
            UpdateTeamPanel(targetPlayer, (Team)value);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.IsMasterClient)
            ConfigureMasterLobbyPanel((State)PhotonNetwork.CurrentRoom.CustomProperties[RoomProperty.GameState]);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(PlayerProperty.Team, Team.None);
        newPlayer.SetCustomProperties(hash);

        UpdateCurrentPlayersCountText();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateCurrentPlayersCountText();

        GameObject go;
        if (playersTemplates.TryGetValue(otherPlayer, out go))
        {
            Destroy(go);
            playersTemplates.Remove(otherPlayer);
        }
    }

    public void UpdateTimeLimitDropdown()
    {
        int time = (timeLimitDropdown.value + 1) * 60;
        gl.ChangeTimeLimit(time);
    }

    public void UpdateScoreLimitDropdown()
    {
        int score = scoreLimitDropdown.value + 1;
        gl.ChangeScoreLimit(score);
    }

    void UpdateCurrentPlayersCountText()
    {
        currentPlayersCountText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
    }

    void UpdateTeamPanel(Player player, Team team = Team.None)
    {
        GameObject go;
        if (!playersTemplates.TryGetValue(player, out go))
        {
            switch (team)
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

            go.GetComponent<NickNameTemplatePanel>().SetUpTemplate(player);
            playersTemplates.Add(player, go);
        }
        else
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

        Debug.Log("Update team panel");
    }

    void ConfigureTeamPanel()
    {
        foreach (Transform child in noTeamPanel)
            Destroy(child.gameObject);

        foreach (var p in PhotonNetwork.CurrentRoom.Players)
        {
            if (p.Value.CustomProperties.Count <= 0)
            {
                GameObject go1 = Instantiate(playerTemplate, noTeamPanel);
                go1.GetComponent<NickNameTemplatePanel>().SetUpTemplate(p.Value);
                playersTemplates.Add(p.Value, go1);
                continue;
            }

            Team team = (Team)p.Value.CustomProperties[PlayerProperty.Team];

            GameObject go;
            switch (team)
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
            go.GetComponent<NickNameTemplatePanel>().SetUpTemplate(p.Value);
            playersTemplates.Add(p.Value, go);
        }
    }

    void Toggle()
    {
        gameLobbyPanel.SetActive(!gameLobbyPanel.activeInHierarchy);
    }

    public void SetActive(bool active)
    {
        gameLobbyPanel.SetActive(active);
    }

    void ConfigurePlayerLobbyPanel()
    {
        startGameButton.SetActive(false);
        stopGameButton.SetActive(false);
        pauseGameButton.SetActive(false);

        timeLimitDropdown.gameObject.SetActive(false);
        scoreLimitDropdown.gameObject.SetActive(false);
        timeLimitText.gameObject.SetActive(true);
        scoreLimitText.gameObject.SetActive(true);
    }

    void ConfigureMasterLobbyPanel(State state)
    {
        switch(state)
        {
            case State.NotStarted:
                startGameButton.SetActive(true);
                stopGameButton.SetActive(false);
                pauseGameButton.SetActive(false);

                timeLimitText.gameObject.SetActive(false);
                scoreLimitText.gameObject.SetActive(false);
                timeLimitDropdown.gameObject.SetActive(true);
                scoreLimitDropdown.gameObject.SetActive(true);
                break;

            case State.Play:
                startGameButton.SetActive(false);
                stopGameButton.SetActive(true);
                pauseGameButton.SetActive(true);

                timeLimitDropdown.gameObject.SetActive(false);
                scoreLimitDropdown.gameObject.SetActive(false);
                timeLimitText.gameObject.SetActive(true);
                scoreLimitText.gameObject.SetActive(true);
                break;
        }
    }

    /*public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    public void UpdateCurrentPlayersCountText(int count)
    {
        currentPlayersCountText.text = count.ToString();
    }

    public void UpdateMaxPlayersCountText(int count)
    {
        maxPlayersCountText.text = count.ToString();
    }

    public void UpdateTimeLimitDropdown(Dropdown dd)
    {
        int time = (dd.value + 1) * 60;
        gl.ChangeTimeLimit(time);
    }

    public void UpdateScoreLimitDropdown(Dropdown dd)
    {
        int score = (dd.value + 1);
        gl.ChangeScoreLimit(score);
    }

    public void AddPlayerToTeamPanel(Team team, string nickName)
    {
        GameObject go;
        switch(team)
        {
            case Team.Blue:
                go = Instantiate(gameLobbyPlayer, blueTeamPanel);
                break;

            case Team.Red:
                go = Instantiate(gameLobbyPlayer, redTeamPanel);
                break;

            default:
                go = Instantiate(gameLobbyPlayer, noTeamPanel);
                break;
        }
        go.GetComponentInChildren<Text>().text = nickName;
    }

    public void ClearTeamPanel(Team team)
    {
        switch (team)
        {
            case Team.Blue:
                foreach (Transform child in blueTeamPanel)
                    Destroy(child);
                break;

            case Team.Red:
                foreach (Transform child in redTeamPanel)
                    Destroy(child);
                break;

            default:
                foreach (Transform child in noTeamPanel)
                    Destroy(child);
                break;
        }
    }

    public void UpdateGameStateButtons(State state)
    {
        switch(state)
        {
            case State.End:
                startGameButton.SetActive(true);
                stopGameButton.SetActive(false);
                pauseGameButton.SetActive(false);
                break;

            case State.Pause:
                startGameButton.SetActive(false);
                stopGameButton.SetActive(true);
                pauseGameButton.SetActive(true);
                break;

            case State.Play:
                startGameButton.SetActive(false);
                stopGameButton.SetActive(true);
                pauseGameButton.SetActive(true);
                break;
        }
    }*/
}
