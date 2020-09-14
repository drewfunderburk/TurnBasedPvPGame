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
        private Item _item;

        public Player()
        {
            _name = "None";
            _combatClass = "None";
            _maxHealth = 100;
            _level = 1;
            _damage = 10;
            _armor = 0;
        }

        public Player(string name, string combatClass, int maxHealth, int level, int damage, int armor, Item item)
        {
            _name = name;
            _combatClass = combatClass;
            _maxHealth = maxHealth;
            _health = maxHealth;
            _level = level;
            _damage = damage;
            _armor = armor;
            _item = item;
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

        public void ConsumeItem()
        {
            if (_item.name == "Health Potion")
            {
                Heal(_item.buff);
            }
            else if (_item.name == "Armor Potion")
            {
                IncreaseArmor(_item.buff);
            }
            else
            {
                return;
            }
            _item.name = "None";
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
