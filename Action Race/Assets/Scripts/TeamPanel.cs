﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TeamPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] RectTransform blueTeamPanel;
    [SerializeField] RectTransform specTeamPanel;
    [SerializeField] RectTransform redTeamPanel;
    [SerializeField] GameObject playerTemplateGO;
    [SerializeField] GameObject moveBlueTeamToSpectGO;
    [SerializeField] GameObject moveRedTeamToSpectGO;
    [SerializeField] GameObject swapTeamsGO;
    [SerializeField] GameObject randTeamsGO;

    Dictionary<int, GameObject> playersTemplates = new Dictionary<int, GameObject>();

    public RectTransform BlueTeamPanel { get { return blueTeamPanel; } }
    public RectTransform SpecTeamPanel { get { return specTeamPanel; } }
    public RectTransform RedTeamPanel { get { return redTeamPanel; } }

    public void AddPlayer(int actorNumber, string nickName, bool isLocal, bool isMasterClient, Team team)
    {
        GameObject go;
        switch(team)
        {
            case Team.Blue:
                go = Instantiate(playerTemplateGO, blueTeamPanel);
                break;

            case Team.Red:
                go = Instantiate(playerTemplateGO, redTeamPanel);
                break;

            default:
                go = Instantiate(playerTemplateGO, specTeamPanel);
                break;
        }

        PlayerTemplatePanel playerTemplatePanel = go.GetComponent<PlayerTemplatePanel>();
        playerTemplatePanel.ActorNumber = actorNumber;
        playerTemplatePanel.NickName = nickName;
        playerTemplatePanel.IsLocal = isLocal;
        playerTemplatePanel.IsMasterClient = isMasterClient;
        playersTemplates.Add(actorNumber, go);
    }

    public void RemovePlayer(int actorNumber)
    {
        GameObject go;
        if (playersTemplates.TryGetValue(actorNumber, out go))
        {
            Destroy(go);
            playersTemplates.Remove(actorNumber);
        }
    }

    public void ChangePlayerTeam(int actorNumber, Team team)
    {
        GameObject go;
        if (playersTemplates.TryGetValue(actorNumber, out go))
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
                    go.transform.SetParent(specTeamPanel);
                    break;
            }
        }
    }

    public void UpdateNewMasterClient(int actorNumber)
    {
        GameObject go;
        if (playersTemplates.TryGetValue(actorNumber, out go))
        {
            PlayerTemplatePanel playerTemplatePanel = go.GetComponent<PlayerTemplatePanel>();
            playerTemplatePanel.IsMasterClient = true;
        }
    }

    public void ConfigureAccess(bool isMasterClient, State state = State.Stop)
    {
        if (isMasterClient && state == State.Stop)
        {
            moveBlueTeamToSpectGO.SetActive(true);
            moveRedTeamToSpectGO.SetActive(true);
            swapTeamsGO.SetActive(true);
            randTeamsGO.SetActive(true);
        }
        else
        {
            moveBlueTeamToSpectGO.SetActive(false);
            moveRedTeamToSpectGO.SetActive(false);
            swapTeamsGO.SetActive(false);
            randTeamsGO.SetActive(false);
        }
    }

    public Dictionary<int, GameObject> GetPlayersTemplates()
    {
        return playersTemplates;
    }
}
