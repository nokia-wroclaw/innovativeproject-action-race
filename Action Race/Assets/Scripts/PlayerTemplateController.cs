using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerTemplatePanel))]
public class PlayerTemplateController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    PlayerTemplatePanel playerTemplatePanel;
    TeamPanel teamPanel;

    RectTransform canvas;
    RectTransform currentParent;

    void Awake()
    {
        playerTemplatePanel = GetComponent<PlayerTemplatePanel>();
        teamPanel = FindObjectOfType<TeamPanel>();

        canvas = FindObjectOfType<Canvas>().gameObject.transform as RectTransform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.GetPlayer(playerTemplatePanel.ActorNumber) != PhotonNetwork.LocalPlayer)
            return;

        currentParent = transform.parent as RectTransform;
        transform.SetParent(canvas);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.GetPlayer(playerTemplatePanel.ActorNumber) != PhotonNetwork.LocalPlayer)
            return;

        RectTransform draggingPlane = eventData.pointerEnter.transform as RectTransform;
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingPlane, eventData.position, eventData.pressEventCamera, out globalMousePos))
            transform.position = globalMousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.GetPlayer(playerTemplatePanel.ActorNumber) != PhotonNetwork.LocalPlayer)
            return;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        FindObjectOfType<EventSystem>().RaycastAll(eventData, raycastResults);

        foreach (var rr in raycastResults)
        {
            RectTransform hitPanel = rr.gameObject.transform as RectTransform;
            if (ChangeTeam(hitPanel, teamPanel.BlueTeamPanel, Team.Blue))
                return;
            else if (ChangeTeam(hitPanel, teamPanel.RedTeamPanel, Team.Red))
                return;
            else if (ChangeTeam(hitPanel, teamPanel.SpecTeamPanel, Team.None))
                return;
        }

        transform.SetParent(currentParent);
    }

    bool ChangeTeam(RectTransform hitPanel, RectTransform newTeamPanel, Team team)
    {
        if (hitPanel == newTeamPanel.parent.parent)
        {
            transform.SetParent(newTeamPanel);
            ChangePlayerTeam(team);
            return true;
        }
        return false;
    }

    public void ChangePlayerTeam(Team team)
    {
        ExitGames.Client.Photon.Hashtable teamProperty = new ExitGames.Client.Photon.Hashtable();
        teamProperty.Add(PlayerProperty.Team, team);
        PhotonNetwork.CurrentRoom.GetPlayer(playerTemplatePanel.ActorNumber).SetCustomProperties(teamProperty);
    }
}
