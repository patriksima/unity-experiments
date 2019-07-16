using UnityEngine;

namespace Dupa
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        public new string name = "New Item";
        public Sprite icon = null;
        public int stack = 1;
    }
}