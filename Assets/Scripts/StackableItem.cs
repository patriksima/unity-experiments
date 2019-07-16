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
            int remaining = 0;

            amount = amount + add;

            if (amount > Stack)
            {
                amount = Stack;
                remaining = add - amount;
            }

            return remaining;
        }
    }
}