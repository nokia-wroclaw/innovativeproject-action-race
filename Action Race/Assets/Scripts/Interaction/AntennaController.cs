using Photon.Pun;
using UnityEngine;

public class AntennaController : MonoBehaviour
{
    [SerializeField] float programmingTimeDuration = 5f;

    Team actualTeam, newTeam;
    bool isProgrammed, isFinished;
    float programmingTime;

    Animator animator;
    PhotonView pv;

    GamePlayManager gamePlayManager;
    GameObject player;

    void Start()
    {
        animator = GetComponent<Animator>();
        pv = GetComponent<PhotonView>();

        gamePlayManager = FindObjectOfType<GamePlayManager>();

        animator.SetFloat("ProgramSpeedMultiplier", 1.0f / programmingTimeDuration);
    }

    public void Update()
    {
        Animate();

        if (isProgrammed)
        {
            programmingTime += Time.deltaTime;

            if(programmingTime >= programmingTimeDuration)
            {
                FinishProgram();

                PlayerInteraction pi = player.GetComponent<PlayerInteraction>();
                if (pi.IsProgramming()) pi.StopProgram();
            }
        }
    }

    void Animate()
    {
        animator.SetInteger("ActualTeam", (int)actualTeam);
        animator.SetInteger("NewTeam", (int)newTeam);
        animator.SetBool("Program", isProgrammed);
    }

    public bool CanProgram(Team team)
    {
        if (team == actualTeam || isProgrammed) return false;
        return true;
    }

    [PunRPC]
    public void StartProgram(Team team, int viewID)
    {
        newTeam = team;
        programmingTime = 0f;
        player = PhotonNetwork.GetPhotonView(viewID).gameObject;

        isProgrammed = true;
    }

    [PunRPC]
    public void StopProgram()
    {
        isProgrammed = false;
    }

    void FinishProgram()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            gamePlayManager.SetScore(newTeam, 1);
            gamePlayManager.SetScore(actualTeam, -1);
        }

        actualTeam = newTeam;
        isFinished = true;
    }
}
