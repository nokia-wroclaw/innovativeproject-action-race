using UnityEngine;
using Photon.Pun;

public class PlayerInteraction : MonoBehaviour
{
    bool isProgramming;

    Animator animator;
    PhotonView pv;
    PlayerTeam pt;

    AntennaController ac;

    void Start()
    {
        animator = GetComponent<Animator>();
        pv = GetComponent<PhotonView>();
        pt = GetComponent<PlayerTeam>();
    }

    void Update()
    {
        if (!pv.IsMine) return;

        Animate();

        if (isProgramming && Input.GetKeyUp(KeyCode.E))
        {
            StopProgram();
            ac.GetComponent<PhotonView>().RPC("StopProgram", RpcTarget.All);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (!pv.IsMine) return;

        if (!isProgramming && Input.GetKeyDown(KeyCode.E))
        {
            ac = col.GetComponent<AntennaController>();
            if (ac && ac.CanProgram(pt.GetTeam()))
            {
                StartProgram();
                ac.GetComponent<PhotonView>().RPC("StartProgram", RpcTarget.All, pt.GetTeam(), pv.ViewID);
            }
        }
    }

    void Animate()
    {
        animator.SetBool("Interact", isProgramming);
    }

    void StartProgram()
    {
        isProgramming = true;
        GetComponent<PlayerMovement>().enabled = false;
    }

    public void StopProgram()
    {
        isProgramming = false;
        GetComponent<PlayerMovement>().enabled = true;
    }

    public bool IsProgramming()
    {
        return isProgramming;
    }
}
