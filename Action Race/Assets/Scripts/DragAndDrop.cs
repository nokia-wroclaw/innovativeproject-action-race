using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    RectTransform currentParent;
    TeamPanelController tpc;

    public void OnBeginDrag(PointerEventData eventData)
    {
        currentParent = transform.parent as RectTransform;
        transform.SetParent(FindObjectOfType<Canvas>().transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform draggingPlane = eventData.pointerEnter.transform as RectTransform;
        RectTransform rt = GetComponent<RectTransform>();
        Vector3 globalMousePos;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingPlane as RectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
            rt.position = globalMousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        FindObjectOfType<EventSystem>().RaycastAll(eventData, raycastResults);

        TeamPanel tp;
        foreach (var r in raycastResults)
        {
            if(tp = r.gameObject.GetComponent<TeamPanel>())
            {
                tp.ChangeTeam(gameObject);
                return;
            }
        }
        transform.SetParent(currentParent);
    }
}
