using System;
using System.Collections.Generic;
using System.Text;

namespace TurnBasedPvPGame
{
    class Inventory
    {
        Item[] _inventory;

        #region CONSTRUCTORS
        public Inventory()
        {
            _inventory = new Item[10];
        }

        public Inventory(int size)
        {
            _inventory = new Item[size];
        }
        public Inventory(Item[] items)
        {
            _inventory = new Item[items.Length];
            Array.Copy(items, _inventory, items.Length);
        }

        public Inventory(int size, Item[] items)
        {
            _inventory = new Item[size];
            Array.Copy(items, _inventory, items.Length);
        }
        #endregion

        #region GETTERS AND SETTERS
        public Item GetItemAtSlot(int slot)
        {
            if (slot < 0 || slot >= _inventory.Length)
                return _inventory[0];

            return _inventory[slot];
        }

        public void SetItemAtSlot(int slot, Item item)
        {
            if (slot < 0 || slot >= _inventory.Length)
                return;

            _inventory[slot] = item;
        }

        public Item[] GetInventoryArray()
        {
            return _inventory;
        }
        #endregion

        public void PrintInventory()
        {
            for (int i = 0; i < _inventory.Length; i++)
            {
                Console.Write(" [" + (i + 1) + "] " + _inventory[i].name);
                if (_inventory[i].name != "None")
                    Console.Write(": " + _inventory[i].description);
            }
        }

    }
}
