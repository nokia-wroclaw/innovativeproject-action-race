using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class PlayerKick : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float kickCooldown = 1f;

    [Header("References")]
    [SerializeField] CircleCollider2D foot;


    Animator animator;
    PhotonView pv;
    PlayerAntennaProgramming pap;

    float kickDelay = 0f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        pv = GetComponent<PhotonView>();
        pap = GetComponent<PlayerAntennaProgramming>();
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

                int layerMask = LayerMask.GetMask("Player");
                List<Collider2D> colliders = new List<Collider2D>();
                foot.OverlapCollider(new ContactFilter2D() { layerMask = layerMask }, colliders);
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
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 20);

        if (pap.IsProgrammingAntenna())
        {
            pap.StopProgram();
            pap.GetProgrammableAntenna().GetComponent<PhotonView>().RPC("StopProgram", RpcTarget.AllViaServer);
        }
    }
}
