using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using System.Collections.Generic;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    RectTransform currentParent;

    bool IsPlaying()
    {
        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
        object value;

        if (hash.TryGetValue(RoomProperty.GameState, out value))
        {
            if ((State)value == State.NotStarted) 
                return false;
            else 
                return true;
        }
        else
            return false;
    }

    bool IsLocal()
    {
        return GetComponent<PlayerTemplate>().GetPlayer().IsLocal;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!IsPlaying() && (IsLocal() || PhotonNetwork.IsMasterClient))
        {
            currentParent = transform.parent as RectTransform;
            transform.SetParent(FindObjectOfType<Canvas>().transform);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!IsPlaying() && (IsLocal() || PhotonNetwork.IsMasterClient))
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
        if (!IsPlaying() && (IsLocal() || PhotonNetwork.IsMasterClient))
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
