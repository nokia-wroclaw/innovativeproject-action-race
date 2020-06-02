using UnityEngine;
using UnityEngine.UI;

public class LogsPanel : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] string enterLog = "entered the room!";
    [SerializeField] string leaveLog = "leaved the room!";

    [Header("References")]
    [SerializeField] RectTransform logsPanel;
    [SerializeField] GameObject logTemplateGO;

    public void LogPlayerEnter(string nickName)
    {
        GameObject go = Instantiate(logTemplateGO, logsPanel);
        go.GetComponent<Text>().text = nickName + " " + enterLog;
    }

    public void LogPlayerLeave(string nickName)
    {
        GameObject go = Instantiate(logTemplateGO, logsPanel);
        go.GetComponent<Text>().text = nickName + " " + leaveLog;
    }
}
