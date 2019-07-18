using UnityEngine;

namespace Dupa
{
    public class StackableItem
    {
        #region Public handlers
        public delegate void ItemChangedHandler(StackableItem item);
        public static event ItemChangedHandler OnItemChanged;
        #endregion

        #region Public variables
        public Item item;
        public int Stack { get { return item.stack; } }
        public int Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
                OnItemChanged?.Invoke(this);
            }
        }
        public float Durability
        {
            get
            {
                return durability;
            }
            set
            {
                durability = value;
                OnItemChanged?.Invoke(this);
            }
        }
        #endregion

        #region Private variables
        private int amount = 0;
        private float durability = 1f;
        #endregion

        #region Public methods
        public int AddAmount(int add)
        {
            int remaining = 0;

            if (add + Amount > Stack)
            {
                remaining = add + Amount - Stack;
                Amount = Stack;
            }
            else
            {
                Amount = Amount + add;
            }

            return remaining;
        }

        public StackableItem Split()
        {
            int total = Amount;

            Amount = (int)(Amount / 2);

            StackableItem splitted = new StackableItem
            {
                item = item,
                Amount = total - Amount
            };
            return splitted;
        }
        #endregion
    }
}