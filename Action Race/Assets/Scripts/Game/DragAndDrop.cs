using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using System.Collections.Generic;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    PlayerTemplate pt;

    RectTransform canvas;
    RectTransform currentParent;

    void Awake()
    {
        pt = GetComponent<PlayerTemplate>();

        canvas = FindObjectOfType<Canvas>().transform as RectTransform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!pt.IsLocal && !PhotonNetwork.IsMasterClient) return;

        currentParent = transform.parent as RectTransform;
        transform.SetParent(canvas);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!pt.IsLocal && !PhotonNetwork.IsMasterClient) return;

        RectTransform draggingPlane = eventData.pointerEnter.transform as RectTransform;
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingPlane, eventData.position, eventData.pressEventCamera, out globalMousePos))
            transform.position = globalMousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!pt.IsLocal && !PhotonNetwork.IsMasterClient) return;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        FindObjectOfType<EventSystem>().RaycastAll(eventData, raycastResults);

        foreach (var rr in raycastResults)
        {
            TeamPanel tp = rr.gameObject.GetComponent<TeamPanel>();
            if (tp)
            {
                tp.ChangePanel(gameObject, pt.ActorNumber);
                return;
            }
        }

        transform.SetParent(currentParent);
    }
}
