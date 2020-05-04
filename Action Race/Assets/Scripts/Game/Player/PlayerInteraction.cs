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

    // ANTENNA PROGRAMMING
    bool isTouchingAntenna, isProgrammingAntenna;
    Antenna a;

    // KICK
    float kickDelay = 0f;

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
        Kick();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!pv.IsMine) return;

        if (collision.tag == "Antenna")
        {
            a = collision.GetComponent<Antenna>();
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

            if (a.CanProgram(team))
            {
                StartProgram();
                a.GetComponent<PhotonView>().RPC("StartProgram", RpcTarget.AllViaServer, team, 0f, pv.ViewID);
            }
        }

        // STOP PROGRAM
        if (isProgrammingAntenna && Input.GetKeyUp(KeyCode.E))
        {
            StopProgram();
            a.GetComponent<PhotonView>().RPC("StopProgram", RpcTarget.AllViaServer);
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

    public Antenna GetProgrammableAntenna()
    {
        return a;
    }

    [PunRPC]
    void TakeKick(int viewId)
    {
        PhotonView otherPV = PhotonNetwork.GetPhotonView(viewId);
        otherPV.GetComponent<Rigidbody2D>().velocity += new Vector2(0, 20);

        PlayerInteraction otherPI = otherPV.GetComponent<PlayerInteraction>();
        if (otherPI.IsProgrammingAntenna())
        {
            otherPI.StopProgram();
            otherPI.GetProgrammableAntenna().GetComponent<PhotonView>().RPC("StopProgram", RpcTarget.AllViaServer);
        }
    }
}
