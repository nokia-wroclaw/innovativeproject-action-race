using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class AntennaController : MonoBehaviourPunCallbacks
{
    [SerializeField] float programmingTimeDuration = 5f;

    Animator _animator;
    ScoreController scoreController;
    PhotonView pv;

    bool isProgrammed;
    Team currentTeam, newTeam;
    PhotonView playerPhotonView;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        scoreController = FindObjectOfType<ScoreController>();
        pv = GetComponent<PhotonView>();

        _animator.SetFloat("ProgramSpeedMultiplier", 1.0f / programmingTimeDuration);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        pv.RPC("StartProgram", newPlayer, newTeam, _animator.GetCurrentAnimatorStateInfo(0).normalizedTime, playerPhotonView.ViewID);
    }

    public bool CanProgram(Team newTeam)
    {
        if (currentTeam == newTeam || isProgrammed) return false;
        return true;
    }

    [PunRPC]
    public void StartProgram(Team newTeam, float normalizedTime, int viewID)
    {
        if(newTeam != Team.None)
            isProgrammed = true;

        this.newTeam = newTeam;

        if (newTeam != Team.None)
        {
            playerPhotonView = PhotonNetwork.GetPhotonView(viewID);

            string stateName = newTeam == Team.Blue ? "ProgramBlue" : "ProgramRed";
            _animator.Play(stateName, 0, normalizedTime);
        }
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
        _animator.Play(stateName, 0, 1f);
    }

    public void FinishProgram()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            scoreController.AddScore(newTeam, 1);
            scoreController.AddScore(currentTeam, -1);
        }

        isProgrammed = false;
        currentTeam = newTeam;

        if (playerPhotonView.IsMine)
            playerPhotonView.GetComponent<PlayerController>().StopProgram(true);
    }
}
