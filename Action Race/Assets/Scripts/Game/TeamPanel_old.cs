using UnityEngine;

public class TeamPanel_old : MonoBehaviour
{
    [SerializeField] Team team;
    [SerializeField] RectTransform content;

    GameLobbyController glc;

    void Awake()
    {
        glc = FindObjectOfType<GameLobbyController>();
    }

    public void ChangePanel(GameObject go, int actorNumber)
    {
        go.transform.SetParent(content);
        glc.ChangePlayerTeam(actorNumber, team);
    }
}
