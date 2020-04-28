using Photon.Pun;
using UnityEngine;

public class AntennaController : MonoBehaviour
{
    [SerializeField] float programmingTimeDuration = 5f;

    Animator animator;
    GameScore gs;

    bool isProgrammed;
    float programmingTime;
    Team currentTeam, newTeam;
    PlayerInteraction pi;

    void Start()
    {
        animator = GetComponent<Animator>();
        gs = FindObjectOfType<GameScore>();

        animator.SetFloat("ProgramSpeedMultiplier", 1.0f / programmingTimeDuration);
    }

    public void Update()
    {
        Animate();

        if (isProgrammed)
        {
            programmingTime += Time.deltaTime;

            if (programmingTime >= programmingTimeDuration)
            {
                FinishProgram();

                if (pi.IsProgrammingAntenna()) pi.StopProgram();
            }
        }
    }

    void Animate()
    {
        animator.SetInteger("CurrentTeam", (int)currentTeam);
        animator.SetInteger("NewTeam", (int)newTeam);
        animator.SetBool("Program", isProgrammed);
    }

    public bool CanProgram(Team team)
    {
        if (team == currentTeam || isProgrammed) return false;
        return true;
    }

    [PunRPC]
    public void StartProgram(Team team, int viewID)
    {
        newTeam = team;
        programmingTime = 0f;
        pi = PhotonNetwork.GetPhotonView(viewID).GetComponent<PlayerInteraction>();

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
            gs.AddScore(newTeam, 1);
            gs.AddScore(currentTeam, -1);
        }

        currentTeam = newTeam;
    }
}