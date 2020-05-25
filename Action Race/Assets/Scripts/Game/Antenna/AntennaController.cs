using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class AntennaController : MonoBehaviourPunCallbacks
{
    [SerializeField] float programmingTimeDuration = 5f;

    Animator animator;
    GameScoreController gsc;
    PhotonView pv;

    bool isProgrammed;
    Team currentTeam, newTeam;
    int programmerViewID;

    void Awake()
    {
        animator = GetComponent<Animator>();
        gsc = FindObjectOfType<GameScoreController>();
        pv = GetComponent<PhotonView>();

        animator.SetFloat("ProgramSpeedMultiplier", 1.0f / programmingTimeDuration);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        pv.RPC("StartProgram", newPlayer, newTeam, animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 0);
    }

    public bool CanProgram(Team newTeam)
    {
        if (currentTeam == newTeam || isProgrammed) return false;
        return true;
    }

    [PunRPC]
    public void StartProgram(Team newTeam, float normalizedTime, int viewId)
    {
        isProgrammed = true;
        this.newTeam = newTeam;

        if (newTeam == Team.None) return;

        programmerViewID = viewId;
        string stateName = newTeam == Team.Blue ? "ProgramBlue" : "ProgramRed";
        animator.Play(stateName, 0, normalizedTime);
    }

    [PunRPC]
    public void StopProgram()
    {
        isProgrammed = false;

        string stateName;
        switch(currentTeam)
        {
            case Team.Blue:
                stateName = "Blue";
                break;

            case Team.Red:
                stateName = "Red";
                break;

            default:
                stateName = "Neutral";
                break;
        }
        animator.Play(stateName, 0, 1f);
    }

    public void FinishProgram()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            gsc.AddScore(newTeam, 1);
            gsc.AddScore(currentTeam, -1);
        }

        isProgrammed = false;
        currentTeam = newTeam;

        if (!PhotonNetwork.IsMasterClient) return;
        PhotonNetwork.GetPhotonView(programmerViewID).GetComponent<PlayerAntennaProgramming>().StopProgram();
    }
}
