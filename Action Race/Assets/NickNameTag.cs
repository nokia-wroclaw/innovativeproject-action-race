using UnityEngine;
using TMPro;
using Photon.Pun;

public class NickNameTag : MonoBehaviour
{
    [SerializeField] TextMeshPro nickNameTMP;

    void Start()
    {
        nickNameTMP.text = GetComponent<PhotonView>().Owner.NickName;
    }
}
