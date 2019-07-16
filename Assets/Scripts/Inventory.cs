using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dupa
{
    public class Inventory : MonoBehaviour
    {
        private readonly ArrayList items = new ArrayList(8) { null, null, null, null, null, null, null, null };
        private ItemHolder[] holders;
        //private byte active = 0b1;

        private void Awake()
        {
            // populate quickbar
            AssetManager.Instance.OnLoaded += assets =>
            {
                for (int i = 0; i < assets.Count; i++)
                {
                    StackableItem stackable = new StackableItem
                    {
                        item = assets[i],
                        amount = UnityEngine.Random.Range(1, 101)
                    };

                    AddItem(stackable, UnityEngine.Random.Range(0, 8));
                }
            };
        }

        private void Start()
        {
            holders = GetComponentsInChildren<ItemHolder>();
        }
        /*
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
        */

        public void AddItem(StackableItem item, int index)
        {
            items[index] = item;
            UpdateUI();
        }
        /*
        public void Del(int index)
        {
            items[index] = null;
            UpdateUI();
        }

        public void Swap(int source, int target)
        {
            Item tmp = items[target] as Item;
            items[target] = items[source];
            items[source] = tmp;
            UpdateUI();
        }*/

        public void UpdateUI()
        {
            Debug.Log("UpdateSlots");
            for (int i = 0; i < holders.Length; i++)
            {
                /*
                if ((active >> i) == 1)
                {
                    slots[i].SetActive();
                } else
                {
                    slots[i].SetInactive();
                }
                */
                holders[i].SetItem(items[i] as StackableItem);
            }
        }
    }
}