using UnityEngine;

public class MainMenuPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject createRoomPanelGO;

    public void TryCreateRoom()
    {
        gameObject.SetActive(false);
        createRoomPanelGO.SetActive(true);
    }
}
