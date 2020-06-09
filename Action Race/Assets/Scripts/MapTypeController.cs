using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(MapTypePanel))]
public class MapTypeController : MonoBehaviourPunCallbacks
{
    enum MapType
    {
        Small,
        Big
    }

    [Header("Properties")]
    [SerializeField] MapType defaultMapType = MapType.Big;

    MapTypePanel mapTypePanel;

    void Awake()
    {
        mapTypePanel = GetComponent<MapTypePanel>();
    }

    void Start()
    {
        mapTypePanel.ClearDropdown();

        var enumValues = System.Enum.GetValues(typeof(MapType));
        foreach (var val in enumValues)
            mapTypePanel.AddDropdownValue(val.ToString());

        if (PhotonNetwork.IsMasterClient)
        {
            mapTypePanel.Value = defaultMapType.ToString();
            mapTypePanel.ConfigureAccess(true);
        }
        else
        {
            object mapTypeValue;
            ExitGames.Client.Photon.Hashtable customRoomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
            if (customRoomProperties.TryGetValue(RoomProperty.MapType, out mapTypeValue))
                mapTypePanel.Value = (string)mapTypeValue;
            else
                mapTypePanel.Value = defaultMapType.ToString();

            mapTypePanel.ConfigureAccess(false);
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object mapTypeValue;
        if (propertiesThatChanged.TryGetValue(RoomProperty.MapType, out mapTypeValue))
            mapTypePanel.Value = (string)mapTypeValue;

        object gameStateValue;
        if (propertiesThatChanged.TryGetValue(RoomProperty.GameState, out gameStateValue))
        {
            State gameState = (State)gameStateValue;
            mapTypePanel.ConfigureAccess(PhotonNetwork.IsMasterClient, gameState);
        }
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        if (PhotonNetwork.LocalPlayer != newMasterClient) return;

        object gameStateValue;
        ExitGames.Client.Photon.Hashtable customRoomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        if (customRoomProperties.TryGetValue(RoomProperty.GameState, out gameStateValue))
            mapTypePanel.ConfigureAccess(true, (State)gameStateValue);
        else
            mapTypePanel.ConfigureAccess(true, State.Stop);
    }

    public void ChangeMapType(int option)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        string mapType = mapTypePanel.GetMapType(option);
        ExitGames.Client.Photon.Hashtable countdownTimerProperty = new ExitGames.Client.Photon.Hashtable();
        countdownTimerProperty.Add(RoomProperty.MapType, mapType);
        PhotonNetwork.CurrentRoom.SetCustomProperties(countdownTimerProperty);

        int idScene = UnityEngine.SceneManagement.SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + mapType + ".unity");
        PhotonNetwork.LoadLevel(idScene);
    }
}
