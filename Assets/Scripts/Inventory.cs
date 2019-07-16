using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dupa
{
    public class Inventory : MonoBehaviour
    {
        private ArrayList items = new ArrayList(8) { null, null, null, null, null, null, null, null };
        private Slot[] slots;
        private byte active = 0b1;

        private void Start()
        {
            slots = GetComponentsInChildren<Slot>();

            AssetManager.Instance.onLoaded += itemTemplates =>
            {
                Debug.Log("Inventory::Items: " + itemTemplates.Count);

                for (int i = 0; i < itemTemplates.Count; i++)
                {
                    Add(itemTemplates[i], i);
                }
            };
        }

        private void Update()
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                Debug.Log("Mouse ScrollWheel: " + Input.GetAxis("Mouse ScrollWheel").ToString("F3"));
                Debug.Log("Mouse scroll delta: " + Input.mouseScrollDelta.y.ToString("F3"));
                if (Input.mouseScrollDelta.y > 0f)
                {
                    int shift = (int)Math.Round(Input.mouseScrollDelta.y);
                    int temp = active << shift;
                    if (temp > 128)
                        temp = 128;
                    active = Convert.ToByte(temp);
                } else
                {
                    int shift = (int)Math.Round(-Input.mouseScrollDelta.y);
                    int temp = active >> shift;
                    if (temp < 1)
                        temp = 1;
                    active = Convert.ToByte(temp);
                }
                Debug.Log("Active: " +Convert.ToString(active, 2));
                UpdateSlots();
            }
        }

        public void Add(Item item, int index)
        {
            items[index] = item;
            UpdateSlots();
        }

        public void Del(int index)
        {
            items[index] = null;
            UpdateSlots();
        }

        public void Swap(int source, int target)
        {
            Item tmp = items[target] as Item;
            items[target] = items[source];
            items[source] = tmp;
            UpdateSlots();
        }

        public void UpdateSlots()
        {
            Debug.Log("UpdateSlots");
            for (int i = 0; i < slots.Length; i++)
            {
                if ((active >> i) == 1)
                {
                    slots[i].SetActive();
                } else
                {
                    slots[i].SetInactive();
                }
                slots[i].SetItem(items[i] as Item);
            }
        }
    }
}