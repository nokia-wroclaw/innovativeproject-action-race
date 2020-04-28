using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerTag : MonoBehaviour
{
    [SerializeField] TextMeshPro nickNameTMP;

    void Start()
    {
        nickNameTMP.text = GetComponent<PhotonView>().Owner.NickName;
    }
}
