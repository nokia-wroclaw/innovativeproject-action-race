using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameLobbyPanel : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] int defaultTimeLimitDropdownId = 2;
    [SerializeField] int defaultScoreLimitDropdownId = 2;

    [Header("References")]
    [SerializeField] Text playersText;
    [SerializeField] Text maxPlayersText;
    [SerializeField] Text roomNameText;

    [SerializeField] RectTransform blueTeamPanel;
    [SerializeField] RectTransform noTeamPanel;
    [SerializeField] RectTransform redTeamPanel;
    [SerializeField] GameObject playerTemplate;

    [SerializeField] Dropdown timeLimitDropdown;
    [SerializeField] Dropdown scoreLimitDropdown;
    [SerializeField] Text timeLimitText;
    [SerializeField] Text scoreLimitText;

    [SerializeField] GameObject startGameButton;
    [SerializeField] GameObject stopGameButton;
    [SerializeField] GameObject pauseGameButton;

    [SerializeField] GameObject moveBlueToSpec;
    [SerializeField] GameObject moveRedToSpec;

    Dictionary<int, GameObject> playersTemplates = new Dictionary<int, GameObject>();

    public string RoomName
    {
        set { roomNameText.text = value; }
    }

    public int Players
    {
        set { playersText.text = value.ToString(); }
    }

    public int MaxPlayers
    {
        set { maxPlayersText.text = value.ToString(); }
    }

    public int DefaultScoreLimit
    {
        get { return defaultScoreLimitDropdownId + 1; }
    }

    public int DefaultTimeLimit
    {
        get { return defaultTimeLimitDropdownId + 1; }
    }

    public int ScoreLimit
    {
        get { return scoreLimitDropdown.value + 1; }
        set
        {
            scoreLimitDropdown.value = value - 1;
            scoreLimitText.text = scoreLimitDropdown.options[scoreLimitDropdown.value].text;
        }
    }

    public int TimeLimit
    {
        get { return timeLimitDropdown.value + 1; }
        set 
        {
            timeLimitDropdown.value = value - 1;
            timeLimitText.text = timeLimitDropdown.options[timeLimitDropdown.value].text;
        }
    }

    void Start()
    {
        //TEMPORARY OFF
        pauseGameButton.SetActive(false);
        moveBlueToSpec.SetActive(false);
        moveRedToSpec.SetActive(false);
    }

    public void ConfigureMasterClientPanel(State state)
    {
        switch (state)
        {
            case State.Stop:
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

                //moveBlueToSpec.SetActive(false);
                //moveRedToSpec.SetActive(false);
                break;
        }
    }

    public void ConfigureClientPanel()
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

    public void AddPlayer(int actorNumber, string nickName, bool isLocal, bool isMasterClient, Team team)
    {
        GameObject go;
        switch(team)
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

        PlayerTemplate pt = go.GetComponent<PlayerTemplate>();
        pt.ActorNumber = actorNumber;
        pt.NickName = nickName;
        pt.IsLocal = isLocal;
        pt.IsMasterClient = isMasterClient;
        playersTemplates.Add(actorNumber, go);
    }

    public void RemovePlayer(int actorNumber)
    {
        GameObject go;
        if(playersTemplates.TryGetValue(actorNumber, out go))
        {
            Destroy(go);
            playersTemplates.Remove(actorNumber);
        }
    }

    public void ChangePlayerTeam(int actorNumber, Team team)
    {
        GameObject go;
        if(playersTemplates.TryGetValue(actorNumber, out go))
        {
            switch(team)
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
    }

    public void ChangePlayerIsMasterClient(int actorNumber)
    {
        GameObject go;
        if (playersTemplates.TryGetValue(actorNumber, out go))
            go.GetComponent<PlayerTemplate>().IsMasterClient = true;
    }
}
