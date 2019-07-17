using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Dupa
{
    public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler/*, IPointerEnterHandler, IPointerExitHandler*/
    {
        private StackableItem item;

        private Transform parent;
        private Canvas canvas;
        private CanvasGroup group;
        private Slot slot;
        private ItemHolder holder;
        private GameObject placeHolder;

        private bool isBeingDragged = false;

        private void Start()
        {
            slot = GetComponentInParent<Slot>();
            holder = GetComponent<ItemHolder>();
            canvas = GetComponentInParent<Canvas>();
            group = GetComponent<CanvasGroup>();
            parent = transform.parent;
        }

#if UNITY_EDITOR 
        private void Update()
        {
            if (isBeingDragged)
            {
                // Is mouse over UI?
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    GetComponent<Image>().color = Color.green;
                }
                else
                {
                    GetComponent<Image>().color = Color.red;
                }
            }
        }
#endif
        private void UpdateItem()
        {
            item = holder.GetItem();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // Update item from holder
            UpdateItem();

            // empty slot is not draggable
            if (item == null) return;

            isBeingDragged = true;

            // off raycast for bubble mouse event
            group.blocksRaycasts = false;

            // if drag with middle mouse - split stack
            if (eventData.button == PointerEventData.InputButton.Middle)
            {
                Split();
            }

            // set parent to Canvas (drag above all elements)
            transform.SetParent(canvas.transform);
        }

        public void OnDrag(PointerEventData eventData)
        {
            // empty slot is not draggable
            if (item == null) return;

            // magic mouse position to UI screen
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                eventData.position, canvas.worldCamera,
                out Vector2 movePos);

            transform.position = canvas.transform.TransformPoint(movePos);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // empty slot is not draggable
            if (item == null) return;

            isBeingDragged = false;

            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;

            group.blocksRaycasts = true;

            Drop(eventData.pointerEnter);
            Destroy(placeHolder);
        }

        private void Drop(GameObject target)
        {
            if (target == null) return;

            Slot targetSlot = target.GetComponentInParent<Slot>();

            if (targetSlot == slot) return;

            ItemHolder targetHolder = target.GetComponentInParent<ItemHolder>();
            StackableItem targetItem = targetHolder.GetItem();

            // if target item is same type (add)
            if (targetItem != null && item.item.name == targetItem.item.name)
            {
                if (placeHolder == null)
                {
                    // Add amount
                    int remaining = targetItem.AddAmount(item.amount);

                    // Remaining back
                    item.amount = remaining;

                    targetHolder.SetItem(targetItem);

                    if (remaining > 0)
                    {
                        holder.SetItem(item);
                    }
                    else
                    {
                        // nothing left
                        item = null;
                        holder.SetItem(null);
                    }
                }
                else
                {
                    // Add amount
                    int remaining = targetItem.AddAmount(item.amount);

                    // Remaining add to placeholder (stand still on place)
                    StackableItem holdItem = placeHolder.GetComponent<ItemHolder>().GetItem();
                    holdItem.amount += remaining;

                    targetHolder.SetItem(targetItem);

                    if (holdItem.amount > 0)
                    {
                        holder.SetItem(holdItem);
                    }
                    else
                    {
                        // nothing left
                        item = null;
                        holder.SetItem(null);
                    }
                }
            }
            else
            {
                // swap items
                if (placeHolder == null)
                {
                    holder.SetItem(targetItem);
                    targetHolder.SetItem(item);
                }
                else
                {
                    if (targetItem == null)
                    {
                        holder.SetItem(placeHolder.GetComponent<ItemHolder>().GetItem());
                        targetHolder.SetItem(item);
                    }
                    else
                    {
                        // drop is invalid, revert amount back
                        StackableItem holdItem = placeHolder.GetComponent<ItemHolder>().GetItem();
                        item.amount += holdItem.amount;
                        holder.SetItem(item);
                    }
                }
            }
        }

        private void Split()
        {
            // dont split if amount under 2, drag as normal
            if (item.amount < 2) return;

            // split stack to half
            StackableItem splitted = item.Split();

            // set placeholder
            placeHolder = Instantiate(gameObject, transform.parent);
            placeHolder.GetComponent<ItemHolder>().SetItem(splitted);

            //set half to draggable item
            holder.SetItem(item);
        }

        /*
        private Canvas canvas;
        private CanvasGroup canvasGroup;
        private Inventory inventory;
        private Slot slot;
        private bool isBeingDragged = false;
        private Image image;
        private RectTransform rectTransform;
        private Color color;
        private GameObject half;

        private bool isDraggable = false;

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
            if (!isDraggable) return;

            slot.isDragged = true;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                eventData.position, canvas.worldCamera,
                out Vector2 movePos);

            transform.position = canvas.transform.TransformPoint(movePos);
            
        }

        public void UnderMouseObject(PointerEventData eventData) {
        
            List<RaycastResult> results = new List<RaycastResult>();
            canvas.GetComponent<GraphicRaycaster>().Raycast(eventData, results);
            foreach (RaycastResult result in results)
            {
                Debug.Log("Hit " + result.gameObject.name);
            }
            
    }

    public void OnBeginDrag(PointerEventData eventData)
        {
            if (!isDraggable) return;

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
            if (!isDraggable) return;

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
    */
    }
}