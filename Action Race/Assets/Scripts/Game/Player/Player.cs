﻿using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(AudioSource), typeof(PhotonView), typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IPunObservable
{
    [Header("Properties")]
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 22f;
    [SerializeField] Vector2 kickPower = new Vector2(20f, 20f);

    [Header("References")]
    [SerializeField] GameObject playerCamera;
    [SerializeField] GameObject miniMapCamera;
    [SerializeField] TextMesh nickNameTag;
    [SerializeField] PlayerFeet playerFeet;

    AudioSource _audioSource;
    PhotonView _photonView;
    PlayerController _playerController;
    Rigidbody2D _rigidbody;

    public struct InputStr
    {
        public float horizontalSpeed, verticalSpeed;
        public bool hasHorizontalSpeed, isJumping;
        public float gravityScale;
        public bool isFreezed;
    };
    public InputStr input;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _photonView = GetComponent<PhotonView>();
        _playerController = GetComponent<PlayerController>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (!_photonView.IsMine)
        {
            Destroy(GetComponentInChildren<Canvas>().gameObject);
            Destroy(miniMapCamera);
            Destroy(playerCamera);
            Destroy(_playerController);
        }
    }

    void Start()
    {
        SettingsController settingsController = FindObjectOfType<SettingsController>();
        if (settingsController)
        {
            _audioSource.mute = settingsController.Mute;
            _audioSource.volume = settingsController.Volume;
        }

        nickNameTag.GetComponent<MeshRenderer>().sortingLayerName = "Me";
        nickNameTag.text = _photonView.Owner.NickName;
    }

    void Update()
    {
        Run();
        Jump();
        Climb();
    
        PlaySound();
    }

    void Run()
    {
        if (input.hasHorizontalSpeed)
        {
            Vector2 playerVelocity = new Vector2(input.horizontalSpeed * runSpeed, _rigidbody.velocity.y);
            _rigidbody.velocity = playerVelocity;

            nickNameTag.transform.localScale = new Vector3(Mathf.Sign(input.horizontalSpeed), 1f, 1f);
        }
        else if(playerFeet.OnTheGround || playerFeet.IsTouchingLadder)
        {
            Vector2 playerVelocity = new Vector2(0f, _rigidbody.velocity.y);
            _rigidbody.velocity = playerVelocity;
        }
    }

    void Jump()
    {
        if(input.isJumping)
            _rigidbody.velocity = new Vector2(0f, jumpSpeed);
    }

    void Climb()
    {
        _rigidbody.gravityScale = input.gravityScale;
        if(input.gravityScale == 0)
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, input.verticalSpeed * runSpeed);
    }

    void PlaySound()
    {
        //if(input.isJumping)
        //    _audioSource.PlayOneShot(jumpSound);

        //if (input.isKicking)
        //    _audioSource.PlayOneShot(kickSound);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(input.horizontalSpeed);
            stream.SendNext(input.verticalSpeed);
            stream.SendNext(input.hasHorizontalSpeed);
            stream.SendNext(input.isJumping);
            stream.SendNext(input.gravityScale);
        }
        else
        {
            input.horizontalSpeed = (float)stream.ReceiveNext();
            input.verticalSpeed = (float)stream.ReceiveNext();
            input.hasHorizontalSpeed = (bool)stream.ReceiveNext();
            input.isJumping = (bool)stream.ReceiveNext();
            input.gravityScale = (float)stream.ReceiveNext();
        }
    }

    [PunRPC]
    public void GetKick(float dir)
    {
        Vector2 kickVelocity = kickPower;
        kickVelocity.x *= dir;
        _rigidbody.velocity = kickVelocity;

        if (_playerController)
            _playerController.StopProgram();
    }

    [PunRPC]
    public void DestroyObject(int viewID)
    {
        PhotonNetwork.Destroy(PhotonNetwork.GetPhotonView(viewID).gameObject);
    }

    [PunRPC]
    public void Freeze()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " freezed");
        input.isFreezed = true;
    }
}
