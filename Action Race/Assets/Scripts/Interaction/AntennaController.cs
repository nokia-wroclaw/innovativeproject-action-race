using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class AntennaController : MonoBehaviourPunCallbacks
{
    [SerializeField] float programmingTimeDuration = 5f;

    Animator animator;
    GameScore gs;
    PhotonView pv;

    bool isProgrammed;
    float programmingTime;
    Team currentTeam, newTeam;
    PlayerInteraction pi;

    void Start()
    {
        animator = GetComponent<Animator>();
        gs = FindObjectOfType<GameScore>();
        pv = GetComponent<PhotonView>();

        animator.SetFloat("ProgramSpeedMultiplier", 1.0f / programmingTimeDuration);

        //animator.Play(Animator.StringToHash("ProgramBlue"));
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

                if (pi.IsProgrammingAntenna())
                {
                    pi.StopProgram();
                }
            }
        }
    }

    /*public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo(0);
        //pv.RPC("SynchronizeProgram", newPlayer, asi.shortNameHash, asi.normalizedTime);
        pv.RPC("SynchronizeProgram", newPlayer, currentTeam, newTeam, isProgrammed, asi.normalizedTime);
    }*/

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

    [PunRPC]
    //public void SynchronizeProgram(int animationNameHash, float animationNormalizedTime)
    public void SynchronizeProgram(Team currentTeam, Team newTeam, bool isProgrammed, float animationNormalizedTime)
    {
        //GetComponent<Animator>().Play(animationNameHash, 0, 0.5f);
        //Debug.Log(Animator.StringToHash("ProgramBlue") + " " + animationNameHash + " " + animationNormalizedTime);
        //animator.GetCurrentAnimatorStateInfo().no

        GetComponent<Animator>().Play(Animator.StringToHash("ProgramBlue"), 0, 0.8f);

        //animator.Play("", 0, 1)
        //this.currentTeam = currentTeam;
        //this.newTeam = newTeam;
        //this.isProgrammed = isProgrammed;
        //GetComponent<Animator>().ForceStateNormalizedTime(0.5f);
        //animator.GetCurrentAnimatorStateInfo(0).normalizedTime = animationNormalizedTime;
    }

    void FinishProgram()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //gs.AddScore(newTeam, 1);
            //gs.AddScore(currentTeam, -1);
        }

        currentTeam = newTeam;
    }
}