using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class NickNameController : MonoBehaviour
{
    [SerializeField] InputField nickNameIF;

    void Start()
    {
        int number = Random.Range(0, 10000);
        string nickName = "Player" + number;

        nickNameIF.text = nickName;
        ChangeNickName(nickName);
    }

    public void ChangeNickName(string nickName)
    {
        PhotonNetwork.LocalPlayer.NickName = nickName;
    }
}
