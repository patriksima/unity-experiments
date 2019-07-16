using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Dupa
{
    public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private Canvas canvas;
        private CanvasGroup canvasGroup;
        private Inventory inventory;
        private Slot slot;
        private bool isBeingDragged = false;
        private Image image;
        private RectTransform rectTransform;
        private Color color;
        private GameObject half;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            canvasGroup = GetComponent<CanvasGroup>();
            inventory = GetComponentInParent<Inventory>();
            slot = GetComponentInParent<Slot>();
            image = GetComponent<Image>();
            color = image.color;
        }

        private void Update()
        {
            if (isBeingDragged)
            {
                // Is mouse over UI?
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    image.color = Color.green;
                }
                else
                {
                    image.color = Color.red;
                }
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            slot.isDragged = true;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                eventData.position, canvas.worldCamera,
                out Vector2 movePos);

            transform.position = canvas.transform.TransformPoint(movePos);
            /*
            List<RaycastResult> results = new List<RaycastResult>();
            canvas.GetComponent<GraphicRaycaster>().Raycast(eventData, results);
            foreach (RaycastResult result in results)
            {
                Debug.Log("Hit " + result.gameObject.name);
            }
            */
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            isBeingDragged = true;
            canvasGroup.blocksRaycasts = false;


            if (eventData.button == PointerEventData.InputButton.Middle)
            {
                half = Instantiate(gameObject, transform.parent);
                Item old = slot.GetItem();
                int amount = old.amount;
                old.amount = (int)(old.amount / 2);
                slot.SetItem(old);

                old.amount = 66;
                half.GetComponentInParent<Slot>().SetItem(old);
            }

            transform.SetParent(canvas.transform);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            slot.isDragged = false;
            isBeingDragged = false;

            image.color = color;

            transform.SetParent(slot.transform);
            transform.localPosition = Vector3.zero;

            canvasGroup.blocksRaycasts = true;

            // asi lokace kde to upustilo
            GameObject target = eventData.pointerEnter;
            Slot targetSlot = target?.GetComponentInParent<Slot>();

            if (targetSlot != null && slot != targetSlot)
            {
                Item old = targetSlot.SetItem(slot.GetItem());
                if (old == null)
                {
                    slot.RemoveItem();
                }
                else
                {
                    slot.SetItem(old);
                }
            }

            Destroy(half);

            //EventManager.TriggerItemDrop(slot.GetItem());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //print("ItemDragHandler::OnPointerExit:" + eventData);
            slot.SetInactive();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //print("ItemDragHandler::OnPointerEnter:" + eventData);
            slot.SetActive();
        }
    }
}