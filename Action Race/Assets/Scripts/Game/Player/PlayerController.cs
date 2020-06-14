using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

[RequireComponent(typeof(Animator), typeof(Player), typeof(PhotonView))]
public class PlayerController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float kickCooldown = 1f;

    [Header("References")]
    [SerializeField] PlayerBody playerBody;
    [SerializeField] PlayerFeet playerFeet;
    [SerializeField] PlayerKickFoot playerKickFoot;

    Animator _animator;
    Player _movement;
    PhotonView _photonView;

    float kickDelay;
    bool isTouchingAntenna, isProgramming;
    AntennaController antennaController;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Player>();
        _photonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Me";

        _movement.input.gravityScale = 5;
    }

    void Update()
    {
        Run();
        Jump();
        Climb();
        Kick();
        ProgramAntenna();
        FreezeMovement();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Antenna")
        {
            antennaController = collision.GetComponent<AntennaController>();
            isTouchingAntenna = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Antenna")
            isTouchingAntenna = false;
    }

    void Run()
    {
        float horizontalSpeed = isProgramming ? 0f : Input.GetAxis("Horizontal");
        bool isRunning = Mathf.Abs(horizontalSpeed) > 0f;

        _movement.input.horizontalSpeed = horizontalSpeed;
        _movement.input.hasHorizontalSpeed = isRunning;

        if (isRunning)
            transform.localScale = new Vector3(Mathf.Sign(horizontalSpeed), 1f, 1f);
        _animator.SetBool("Run", isRunning);
    }

    void Jump()
    {
        bool isJumping = Input.GetButtonDown("Jump") && playerFeet.OnTheGround && _movement.input.gravityScale != 0 && !isProgramming;

        _movement.input.isJumping = isJumping;

        if(isJumping)
            _animator.SetTrigger("Jump");
    }

    void Climb()
    {
        float verticalSpeed = Input.GetAxis("Vertical");
        bool isClimbing = Mathf.Abs(verticalSpeed) > 0f;
        bool standsOnLadder = playerBody.IsTouchingLadder;

        _movement.input.verticalSpeed = verticalSpeed;
        if (standsOnLadder)
            _movement.input.gravityScale = 0;
        else
            _movement.input.gravityScale = 5;

        _animator.SetBool("Climb", isClimbing && standsOnLadder);
        _animator.SetBool("Stay On Ladder", !isClimbing && standsOnLadder);
    }

    void Kick()
    {
        if (kickDelay > 0f)
            kickDelay -= Time.deltaTime;
        else
        {
            if(Input.GetKeyDown(KeyCode.R) && !isProgramming)
            {
                kickDelay = kickCooldown;
                _animator.SetTrigger("Kick");

                List<Collider2D> colliders = playerKickFoot.CollidingPlayersBodies;
                foreach (Collider2D collider in colliders)
                {
                    PhotonView pvOther = collider.GetComponentInParent<PhotonView>();
                    if (pvOther)
                        pvOther.RPC("GetKick", RpcTarget.AllViaServer, transform.localScale.x);
                }
            }
        }
    }

    void ProgramAntenna()
    {
        if(Input.GetKeyDown(KeyCode.E) && isTouchingAntenna && !isProgramming)
        {
            object teamValue;
            ExitGames.Client.Photon.Hashtable customPlayerProperties = PhotonNetwork.LocalPlayer.CustomProperties;
            if (customPlayerProperties.TryGetValue(PlayerProperty.Team, out teamValue))
            {
                Team team = (Team)teamValue;
                if (antennaController.CanProgram(team))
                {
                    isProgramming = true;
                    antennaController.GetComponent<PhotonView>().RPC("StartProgram", RpcTarget.AllViaServer, team, 0f, _photonView.ViewID);

                    _animator.SetBool("Interact", isProgramming);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
            StopProgram();
    }

    void FreezeMovement()
    {
        if (playerBody.GetNokiaShot)
        {
            _movement.input.hasHorizontalSpeed = false;
            _movement.input.verticalSpeed = 0f;
            _movement.input.horizontalSpeed = 0f;
            _movement.input.isJumping = false;
        }
    }

    public void StopProgram(bool finished = false)
    {
        if (isProgramming)
        {
            isProgramming = false;

            if(!finished)
                antennaController.GetComponent<PhotonView>().RPC("StopProgram", RpcTarget.AllViaServer);

            _animator.SetBool("Interact", isProgramming);
        }
    }
}
