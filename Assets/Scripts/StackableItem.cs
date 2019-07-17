using UnityEngine;

namespace Dupa
{
    public class StackableItem
    {
        public Item item;
        public int Stack { get { return item.stack; } }
        public int amount = 0;
        public float durability = 1f;

        public int AddAmount(int add)
        {
            Debug.Log("StackableItem::AddAmount: add: " + add);
            Debug.Log("StackableItem::AddAmount: amount: " + amount);
            Debug.Log("StackableItem::AddAmount: stack: " + Stack);

            int remaining = 0;

            if (add + amount > Stack)
            {
                remaining = add + amount - Stack;
                amount = Stack;
            }
            else
            {
                amount = amount + add;
            }

            Debug.Log("StackableItem::AddAmount: amount new: " + amount);
            Debug.Log("StackableItem::AddAmount: remaining: " + remaining);

            return remaining;
        }

        public StackableItem Split()
        {
            int total = amount;

            amount = (int)(amount / 2);

            StackableItem splitted = new StackableItem
            {
                item = item,
                amount = total - amount
            };
            return splitted;
        }
    }
}