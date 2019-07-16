using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dupa
{
    public class ItemHolder : MonoBehaviour
    {
        #region Children references
        public Image icon;
        public Image durability;
        public TMPro.TextMeshProUGUI amount;
        #endregion

        private Color transparent = new Color(255f, 255f, 255f, 0f);
        private StackableItem item;

        public void SetItem(StackableItem newItem)
        {
            item = newItem;
            UpdateUI();
        }

        public StackableItem GetItem()
        {
            return item;
        }

        private void UpdateUI()
        {
            if (item == null)
            {
                icon.sprite = null;
                icon.color = transparent;
                icon.enabled = false;
                durability.fillAmount = 0f;
                durability.enabled = false;
                amount.text = "";
                amount.enabled = false;
            } else
            {
                if (item.item.icon == null)
                {
                    icon.sprite = null;
                    icon.color = transparent;
                    icon.enabled = false;
                }
                else
                {
                    icon.sprite = item.item.icon;
                    icon.enabled = true;
                    icon.color = Color.white;
                }

                durability.fillAmount = item.durability;

                amount.text = item.amount.ToString();
                amount.enabled = true;
            }
        }
    }
}
