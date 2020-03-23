using UnityEngine;
using UnityEngine.UI;

public class RoomListFilterPanel : MonoBehaviour
{
    [SerializeField] InputField textFilter;
    [SerializeField] Toggle showFull;
    [SerializeField] Toggle showPrivate;

    private void Start()
    {
        TextFilter = "";
        ShowFull = true;
        ShowPrivate = true;
    }

    public string TextFilter
    {
        get { return textFilter.text; }
        set { textFilter.text = value; }
    }

    public bool ShowFull
    {
        get { return showFull.isOn; }
        set { showFull.isOn = value; }
    }

    public bool ShowPrivate
    {
        get { return showPrivate.isOn; }
        set { showPrivate.isOn = value; }
    }
}
