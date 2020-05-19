using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class PlayerKick : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float kickCooldown = 1f;

    Animator animator;
    PhotonView pv;
    PlayerAntennaProgramming pap;

    PlayerKickFoot playerKickFoot;

    float kickDelay;

    void Awake()
    {
        animator = GetComponent<Animator>();
        pv = GetComponent<PhotonView>();
        pap = GetComponent<PlayerAntennaProgramming>();

        playerKickFoot = GetComponentInChildren<PlayerKickFoot>();
    }

    void Update()
    {
        if (!pv.IsMine) return;
        Kick();
    }

    void Kick()
    {
        if (kickDelay > 0f)
            kickDelay -= Time.deltaTime;
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                kickDelay = kickCooldown;
                animator.SetTrigger("Kick");

                List<Collider2D> colliders = playerKickFoot.CollidingPlayersBodies;
                foreach (Collider2D collider in colliders)
                {
                    PhotonView pvOther = collider.GetComponentInParent<PhotonView>();
                    if (pvOther) pvOther.RPC("TakeKick", RpcTarget.AllViaServer);
                }
            }
        }
    }

    [PunRPC]
    void TakeKick()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(20f, 20f);

        if (pap.IsProgrammingAntenna())
        {
            pap.StopProgram();
            pap.GetProgrammableAntenna().GetComponent<PhotonView>().RPC("StopProgram", RpcTarget.AllViaServer);
        }
    }
}
