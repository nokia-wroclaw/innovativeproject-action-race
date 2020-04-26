using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float kickCooldown = 1f;
    [SerializeField] CircleCollider2D kickFoot;

    Animator animator;
    PhotonView pv;
    PlayerMovement pm;
    PlayerTeam pt;

    // ANTENNA PROGRAMMING
    bool isTouchingAntenna, isProgrammingAntenna;
    AntennaController ac;

    // KICK
    float kickDelay = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        pv = GetComponent<PhotonView>();
        pm = GetComponent<PlayerMovement>();
        pt = GetComponent<PlayerTeam>();
    }

    void Update()
    {
        if (!pv.IsMine) return;

        ProgramAntenna();
        Kick();
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

        if (collision.tag == "Antenna") isTouchingAntenna = false;
    }

    void ProgramAntenna()
    {
        //  START PROGRAM
        if (!isProgrammingAntenna && isTouchingAntenna && Input.GetKeyDown(KeyCode.E))
        {
            if (ac.CanProgram(pt.GetTeam()))
            {
                StartProgram();
                ac.GetComponent<PhotonView>().RPC("StartProgram", RpcTarget.AllBuffered, pt.GetTeam(), pv.ViewID);
            }
        }

        // STOP PROGRAM
        if (isProgrammingAntenna && Input.GetKeyUp(KeyCode.E))
        {
            StopProgram();
            ac.GetComponent<PhotonView>().RPC("StopProgram", RpcTarget.AllBuffered);
        }
    }

    void Kick()
    {
        if (kickDelay > 0f)
        {
            kickDelay -= Time.deltaTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                kickDelay = kickCooldown;
                animator.SetTrigger("Kick");

                int layerMask = LayerMask.GetMask("Player");
                List<Collider2D> colliders = new List<Collider2D>();
                kickFoot.OverlapCollider(new ContactFilter2D() { layerMask = layerMask }, colliders);
                foreach (Collider2D collider in colliders)
                {
                    PhotonView pvOther = collider.GetComponentInParent<PhotonView>();
                    if (pvOther) pv.RPC("TakeKick", RpcTarget.All, pvOther.ViewID);
                }
            }
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

    [PunRPC]
    void TakeKick(int viewId)
    {
        PhotonNetwork.GetPhotonView(viewId).GetComponent<Rigidbody2D>().velocity += new Vector2(0, 30);
    }
}
