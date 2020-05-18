using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class NickNameController : MonoBehaviour
{
    [Header("Custom Scripts References")]
    [SerializeField] CreateRoomPanel createRoomPanel;

    [Header("References")]
    [SerializeField] InputField nickNameIF;

    void Start()
    {
        string nickName;
        if (PlayerPrefs.HasKey("NickName"))
            nickName = PlayerPrefs.GetString("NickName");
        else
        {
            int number = Random.Range(0, 10000);
            nickName = "Player" + number;
        }

        nickNameIF.text = nickName;
        ChangeNickName(nickName);
    }

    public void ChangeNickName(string nickName)
    {
        PlayerPrefs.SetString("NickName", nickName);
        PhotonNetwork.LocalPlayer.NickName = nickName;

        createRoomPanel.RoomName = nickName + "'s room";
    }
}
