using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDragHandler : MonoBehaviour, IDragHandler
{
    private RectTransform holder;

    public void OnDrag(PointerEventData eventData)
    {
        holder?.SendMessage("OnDrag", eventData);
    }

    // Start is called before the first frame update
    void Start()
    {
        holder = GetComponentsInParent<RectTransform>()[1];
    }
}
