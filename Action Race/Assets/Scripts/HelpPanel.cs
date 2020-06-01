using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class HelpPanel : MonoBehaviour
{
    CanvasGroup canvasGroup;

    public bool IsActive
    {
        get { return canvasGroup.alpha == 1f; }
        set
        {
            if (value)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
            }
            else
            {
                canvasGroup.alpha = 0f;
                canvasGroup.blocksRaycasts = false;
            }
        }
    }

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Toggle()
    {
        IsActive = !IsActive;
    }
}
