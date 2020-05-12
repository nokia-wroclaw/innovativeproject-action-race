using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using System.Collections.Generic;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    RectTransform currentParent;

    bool IsLocal()
    {
        return GetComponent<PlayerTemplate>().GetPlayer().IsLocal;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsLocal() || PhotonNetwork.IsMasterClient)
        {
            currentParent = transform.parent as RectTransform;
            transform.SetParent(FindObjectOfType<Canvas>().transform);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsLocal() || PhotonNetwork.IsMasterClient)
        {
            RectTransform draggingPlane = eventData.pointerEnter.transform as RectTransform;
            RectTransform rt = GetComponent<RectTransform>();
            Vector3 globalMousePos;

            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingPlane as RectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
                rt.position = globalMousePos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsLocal() || PhotonNetwork.IsMasterClient)
        {
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            FindObjectOfType<EventSystem>().RaycastAll(eventData, raycastResults);

            TeamPanel tp;
            foreach (var r in raycastResults)
            {
                if (tp = r.gameObject.GetComponent<TeamPanel>())
                {
                    tp.AddToPanel(gameObject);
                    return;
                }
            }
            transform.SetParent(currentParent);
        }
    }
}
