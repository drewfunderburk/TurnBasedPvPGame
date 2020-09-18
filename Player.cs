using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace TurnBasedPvPGame
{
    class Player
    {
        private string _name;
        private string _combatClass;
        private int _maxHealth;
        private int _health;
        private int _level;
        private int _damage;
        private int _armor;
        private Inventory _inventory;

        public Player()
        {
            _name = "None";
            _combatClass = "None";
            _maxHealth = 100;
            _level = 1;
            _damage = 10;
            _armor = 0;
        }

        public Player(string name, string combatClass, int maxHealth, int level, int damage, int armor, Inventory inventory)
        {
            _name = name;
            _combatClass = combatClass;
            _maxHealth = maxHealth;
            _health = maxHealth;
            _level = level;
            _damage = damage;
            _armor = armor;
            _inventory = inventory;
        }

        public void ShowInventory()
        {
            Console.WriteLine("Which Item would you like to use?");
            _inventory.PrintInventory();
        }

        public Inventory GetInventory()
        {
            return _inventory;
        }
        public string GetCombatClass()
        {
            return _combatClass;
        }

        public string GetName()
        {
            return _name;
        }

        public void TakeDamage(int damage)
        {
            damage -= _armor;
            if (damage < 0)
                damage = 0;

            _health -= damage;
            if (_health < 0)
                _health = 0;

            Console.WriteLine(_name + " took " + damage + " damage!");
        }

        public void Heal(int hp)
        {
            _health += hp;
            if (_health > _maxHealth)
                _health = _maxHealth;

            Console.WriteLine(_name + " healed " + hp + " health!");
        }

        public void IncreaseArmor(int armor)
        {
            _armor += armor;
        }

        public void Attack(Player enemy)
        {
            enemy.TakeDamage(_damage);
        }

        public void ConsumeItem(int index)
        {
            Item item = _inventory.GetItemAtSlot(index);
            if (item.name == "Health Potion")
            {
                Heal(item.buff);
            }
            else if (item.name == "Armor Potion")
            {
                IncreaseArmor(item.buff);
            }
            else
            {
                return;
            }
            item.name = "None";
        }

        public bool IsAlive()
        {
            if (_health > 0)
                return true;
            return false;
        }
        
        public void PrintStats()
        {
            Console.WriteLine(_name + " Level " + _level + " " + _combatClass);
            Console.WriteLine(_health + "/" + _maxHealth + "hp | " + _damage + " atk | " + _armor + " armor");
        }
    }
}
