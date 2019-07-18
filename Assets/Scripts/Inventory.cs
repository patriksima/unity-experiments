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
            AssetManager.Instance.OnLoaded += Populate;

            // if any item changed, update ui
            StackableItem.OnItemChanged += itemChanged => UpdateUI();
        }

        private void Start()
        {
            holders = GetComponentsInChildren<ItemHolder>();
        }

        private void Populate(List<Item> assets)
        {
            for (int i = 0; i < assets.Count; i++)
            {
                StackableItem stackable1 = new StackableItem
                {
                    item = assets[i],
                    Amount = UnityEngine.Random.Range(1, assets[i].stack)
                };

                StackableItem stackable2 = new StackableItem
                {
                    item = assets[i],
                    Amount = UnityEngine.Random.Range(1, assets[i].stack)
                };

                int i1 = UnityEngine.Random.Range(0, 8);
                int i2 = UnityEngine.Random.Range(0, 8);

                holders[i1].SetItem(stackable1);
                holders[i2].SetItem(stackable2);
            }

            UpdateUI();
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
                UpdateUI();
            }
        }
        */

        public void UpdateUI()
        {
            for (int i = 0; i < holders.Length; i++)
            {
                /*
                if ((active >> i) == 1)
                {
                    holders[i].SetActive();
                } else
                {
                    holders[i].SetInactive();
                }
                */
                holders[i].UpdateUI();
            }
        }
    }
}