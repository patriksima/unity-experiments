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
    }
}