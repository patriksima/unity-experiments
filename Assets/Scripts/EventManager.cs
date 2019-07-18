using UnityEngine;

namespace Dupa
{
    public class EventManager : MonoBehaviour
    {
        public delegate void EventHandler(GameObject gameObj);
        public static event EventHandler OnEvent;

        public static void TriggerEvent(GameObject gameObj)
        {
            OnEvent?.Invoke(gameObj);
        }
    }
}