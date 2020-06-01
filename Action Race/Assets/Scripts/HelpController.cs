using UnityEngine;

[RequireComponent(typeof(HelpPanel), typeof(CanvasGroup))]
public class HelpController : MonoBehaviour
{
    HelpPanel helpPanel;

    void Awake()
    {
        helpPanel = GetComponent<HelpPanel>();
    }

    void Start()
    {
        helpPanel.IsActive = false;
    }
}
