using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Dupa
{
    public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Color activeColor = Color.white;
        public Color hoverColor = Color.blue;

        private Color origColor;
        private Image image;
        private bool isActive = false;

        private void Start()
        {
            image = GetComponent<Image>();
            origColor = image.color;
        }

        public void Active(bool status)
        {
            isActive = status;

            if (isActive)
            {
                image.color = activeColor;
            }
            else
            {
                image.color = origColor;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            image.color = hoverColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            image.color = origColor;
        }

        /*
        public Image icon;
        public Image durability;
        public TMPro.TextMeshProUGUI amount;

        private Image bgImage;
        private Color original;
        private Item item;
        private bool isActive = false;

        [HideInInspector]
        public bool isDragged = false;
       
        private void Awake()
        {
            EventManager.OnItemDrop += (item) =>
            {
                if (isActive)
                {
                    print("Slot::OnItemDrop:" + item);
                    SetItem(item);
                }
            };
        }
        
        private void Start()
        {
            bgImage = GetComponent<Image>();
            original = bgImage.color;
        }

        private void Update()
        {
            if (isActive)
            {
                Color tmp = bgImage.color;
                tmp.a = 0.6f;
                bgImage.color = tmp;
            }
            else
            {
                bgImage.color = original;
            }
        }

        public void SetActive()
        {
            isActive = true;
        }

        public void SetInactive()
        {
            isActive = false;
        }
       
        public Item SetItem(StackableItem newItem)
        {
            Item old = item;

            item = newItem;

            Color white = Color.white;
            if (item == null || item.amount == 0)
            {
                white.a = 0f;
                icon.sprite = null;
                icon.color = white;
                icon.enabled = false;
                durability.fillAmount = 0f;
                durability.enabled = false;
                amount.text = "";
                amount.enabled = false;
            }
            else
            {
                if (item.icon == null)
                {
                    icon.sprite = null;
                    icon.enabled = false;
                    white.a = 0f;
                    icon.color = white;
                }
                else
                {
                    icon.sprite = item.icon;
                    icon.enabled = true;
                    white.a = 255f;
                    icon.color = white;
                }

                durability.fillAmount = item.durability;

                amount.text = item.amount.ToString();
                amount.enabled = true;
            }

            return old;
        }

        public Item GetItem()
        {
            return item;
        }

        public void RemoveItem()
        {
            item = null;
            Color white = Color.white;
            white.a = 0f;
            icon.sprite = null;
            icon.color = white;
            icon.enabled = false;
            durability.fillAmount = 0f;
            durability.enabled = false;
            amount.text = "";
            amount.enabled = false;
        }

        private void OnMouseDrag()
        {
            bgImage.color = Color.blue;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            bgImage.color = Color.blue;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isActive = false;
            bgImage.color = original;
        }
     */
    }
}