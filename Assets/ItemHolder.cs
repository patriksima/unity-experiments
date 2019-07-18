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

        #region Private variables
        private Color transparent = new Color(255f, 255f, 255f, 0f);
        private StackableItem item;
        #endregion

        public void SetItem(StackableItem newItem)
        {
            item = newItem;
            UpdateUI();
        }

        public StackableItem GetItem()
        {
            return item;
        }

        public void UpdateUI()
        {
            UpdateIcon();
            UpdateDurability();
            UpdateAmount();
        }

        private void UpdateIcon()
        {
            if (item == null || item.item.icon == null)
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
        }

        private void UpdateDurability()
        {
            if (item == null)
            {
                durability.fillAmount = 0f;
                durability.enabled = false;
            }
            else
            {
                durability.fillAmount = item.Durability;
                durability.enabled = true;
            }
        }

        private void UpdateAmount()
        {
            if (item == null)
            {
                amount.text = "";
                amount.enabled = false;
            }
            else
            {
                amount.text = item.Amount.ToString();
                amount.enabled = true;
            }
        }
    }
}
