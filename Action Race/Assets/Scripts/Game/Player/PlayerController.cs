using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

[RequireComponent(typeof(Animator), typeof(Player), typeof(PhotonView))]
public class PlayerController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float kickCooldown = 1f;
    [SerializeField] float stunDuration = 5f;

    [Header("References")]
    [SerializeField] PlayerBody playerBody;
    [SerializeField] PlayerFeet playerFeet;
    [SerializeField] PlayerKickFoot playerKickFoot;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip kickSound;

    Animator _animator;
    AudioSource _audioSource;
    Player _movement;
    PhotonView _photonView;

    float kickDelay;
    bool isTouchingAntenna, isProgramming;
    AntennaController antennaController;
    bool hasNokia;
    float stun;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _movement = GetComponent<Player>();
        _photonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Me";

        _movement.input.gravityScale = 5;

        stun = stunDuration;
    }

    void Update()
    {
        if (_movement.input.isFreezed)
        {
            stun -= Time.deltaTime;

            _movement.input.hasHorizontalSpeed = false;
            _movement.input.verticalSpeed = 0f;
            _movement.input.horizontalSpeed = 0f;
            _movement.input.isJumping = false;

            _animator.SetBool("Run", false);

            if(stun <= 0)
            {
                _movement.input.isFreezed = false;
                stun = stunDuration;
            }
        }
        else
        {
            Run();
            Jump();
            Climb();
            Kick();
            ProgramAntenna();
            ThrowNokia();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Antenna")
        {
            antennaController = collision.GetComponent<AntennaController>();
            isTouchingAntenna = true;
        }
        
        if (collision.tag == "Nokia" && !hasNokia)
        {
            _photonView.RPC("DestroyObject", PhotonNetwork.MasterClient, collision.GetComponent<PhotonView>().ViewID);
            hasNokia = true;
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

        if (isJumping)
        {
            _animator.SetTrigger("Jump");
            _audioSource.PlayOneShot(jumpSound);
        }
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
                _audioSource.PlayOneShot(kickSound);

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

    void ThrowNokia()
    {
        if (Input.GetButtonDown("Throw") && hasNokia)
        {
            float dir = transform.localScale.x;
            Vector3 nokiaPosition = transform.position;
            nokiaPosition.x += dir * 0.7f; //zeby uniknac wlasnego collidera
            GameObject thrownNokia = PhotonNetwork.Instantiate("ThrownNokia", nokiaPosition, Quaternion.identity);
            thrownNokia.GetComponent<ThrownNokiaController>().Throw(dir);
            hasNokia = false;
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
