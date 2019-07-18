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

        private Color original;
        private Transform parent;
        private Canvas canvas;
        private CanvasGroup group;
        private Slot slot;
        private ItemHolder holder;
        private GameObject placeHolder;

        private bool isSplitMode = false;
        private bool isBeingDragged = false;

        private void Start()
        {
            slot = GetComponentInParent<Slot>();
            holder = GetComponent<ItemHolder>();
            canvas = GetComponentInParent<Canvas>();
            group = GetComponent<CanvasGroup>();
            parent = transform.parent;

            original = GetComponent<Image>().color;
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
            else
            {
                GetComponent<Image>().color = original;
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
            if (isSplitMode)
            {
                DropSplit(target);
            }
            else
            {
                DropNormal(target);
            }

            isSplitMode = false;
        }

        private void DropNormal(GameObject target)
        {
            // no target no rockenroll
            if (target == null)
            {
                return;
            }

            Slot targetSlot = target.GetComponentInParent<Slot>();

            // same slot no rockenroll
            if (targetSlot == slot)
            {
                return;
            }

            ItemHolder targetHolder = target.GetComponentInParent<ItemHolder>();
            StackableItem targetItem = targetHolder.GetItem();

            // if target item is same type (add)
            if (targetItem != null && item.item.name == targetItem.item.name)
            {
                // Add amount
                int remaining = targetItem.AddAmount(item.Amount);

                // Remaining back
                item.Amount = remaining;

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
                // swap entire items
                holder.SetItem(targetItem);
                targetHolder.SetItem(item);
            }
        }

        private void DropSplit(GameObject target)
        {
            if (target == null)
            {
                // invalid middle drop, revert amount back
                StackableItem holdItem = placeHolder.GetComponent<ItemHolder>().GetItem();
                item.Amount += holdItem.Amount;
                return;
            }

            Slot targetSlot = target.GetComponentInParent<Slot>();

            if (targetSlot == slot)
            {
                // middle drop back to position, revert amount back
                StackableItem holdItem = placeHolder.GetComponent<ItemHolder>().GetItem();
                item.Amount += holdItem.Amount;
                return;
            }

            ItemHolder targetHolder = target.GetComponentInParent<ItemHolder>();
            StackableItem targetItem = targetHolder.GetItem();

            // if target item is same type (add)
            if (targetItem != null && item.item.name == targetItem.item.name)
            {
                // Add amount
                int remaining = targetItem.AddAmount(item.Amount);

                // Remaining add to placeholder (stand still on place)
                StackableItem holdItem = placeHolder.GetComponent<ItemHolder>().GetItem();
                holdItem.Amount += remaining;

                targetHolder.SetItem(targetItem);

                if (holdItem.Amount > 0)
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
                    item.Amount += holdItem.Amount;
                    holder.SetItem(item);
                }
            }
        }

        private void Split()
        {
            // dont split if amount under 2, drag as normal
            if (item.Amount < 2) return;

            // split stack to half
            StackableItem splitted = item.Split();

            // set placeholder (stay put)
            placeHolder = Instantiate(gameObject, transform.parent);
            placeHolder.GetComponent<ItemHolder>().SetItem(splitted);

            isSplitMode = true;
        }
    }
}