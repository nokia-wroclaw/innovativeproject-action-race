using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class PlayerTemplate : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text masterClientText;
    [SerializeField] Text nickNameText;
    [SerializeField] Text pingText;

    bool _isLocal, _isMasterClient;

    public int ActorNumber { get; set; }

    public string NickName
    {
        set { nickNameText.text = value; }
    }

    public bool IsLocal
    {
        get { return _isLocal; }
        set
        {
            _isLocal = value;

            if(value)
                image.color = new Color(255, 255, 0); 
        }
    }

    public bool IsMasterClient
    {
        get { return _isMasterClient; }
        set 
        {
            _isMasterClient = value;

            if(_isMasterClient)
                masterClientText.text = "Host";
        }
    }

    public void SetPing(string ping)
    {
        pingText.text = ping;
    }
}
