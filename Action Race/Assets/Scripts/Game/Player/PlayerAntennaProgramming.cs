using UnityEngine;
using Photon.Pun;

public class PlayerAntennaProgramming : MonoBehaviour
{
    Animator animator;
    PhotonView pv;
    PlayerMovement pm;


    bool isTouchingAntenna, isProgrammingAntenna;
    AntennaController ac;

    void Awake()
    {
        animator = GetComponent<Animator>();
        pv = GetComponent<PhotonView>();
        pm = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!pv.IsMine) return;

        ProgramAntenna();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!pv.IsMine) return;

        if (collision.tag == "Antenna")
        {
            ac = collision.GetComponent<AntennaController>();
            isTouchingAntenna = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!pv.IsMine) return;

        if (collision.tag == "Antenna")
        {
            isTouchingAntenna = false;
        }
    }

    void ProgramAntenna()
    {
        //  START PROGRAM
        if (!isProgrammingAntenna && isTouchingAntenna && Input.GetKeyDown(KeyCode.E))
        {
            Team team;

            ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;
            object value;
            if (hash.TryGetValue(PlayerProperty.Team, out value))
                team = (Team)value;
            else
                team = Team.None;

            if (ac.CanProgram(team))
            {
                StartProgram();
                ac.GetComponent<PhotonView>().RPC("StartProgram", RpcTarget.AllViaServer, team, 0f, pv.ViewID);
            }
        }

        // STOP PROGRAM
        if (isProgrammingAntenna && Input.GetKeyUp(KeyCode.E))
        {
            StopProgram();
            ac.GetComponent<PhotonView>().RPC("StopProgram", RpcTarget.AllViaServer);
        }
    }

    void StartProgram()
    {
        isProgrammingAntenna = true;
        animator.SetBool("Interact", true);

        pm.StopMovement();
        pm.enabled = false;
    }

    public void StopProgram()
    {
        isProgrammingAntenna = false;
        animator.SetBool("Interact", false);

        pm.enabled = true;
    }

    public bool IsProgrammingAntenna()
    {
        return isProgrammingAntenna;
    }

    public AntennaController GetProgrammableAntenna()
    {
        return ac;
    }
}
