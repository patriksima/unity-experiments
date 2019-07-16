using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dupa
{
    public class EventManager : MonoBehaviour
    {
        public delegate void ItemDrop(Item item);
        public static event ItemDrop OnItemDrop;

        public static void TriggerItemDrop(Item item)
        {
            OnItemDrop?.Invoke(item);
        }
    }
}