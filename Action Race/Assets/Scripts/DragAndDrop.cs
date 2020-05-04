using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        gameObject.transform.SetParent(FindObjectOfType<Canvas>().transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var m_DraggingPlane = eventData.pointerEnter.transform as RectTransform;

        var rt = GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane as RectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
            rt.position = globalMousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerDrag);
        //Debug.Log(eventData.pointerCurrentRaycast);
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        FindObjectOfType<EventSystem>().RaycastAll(eventData, raycastResults);
        foreach (var v in raycastResults) Debug.Log(v);
    }
}
