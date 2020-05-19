using UnityEngine;
using System.Collections;

public class MainMenuPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject createRoomPanelGO;

    public IEnumerator TryCreateRoom()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        createRoomPanelGO.SetActive(true);
    }
}
