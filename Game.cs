using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace TurnBasedPvPGame
{
    class Game
    {
        bool _endGame = false;
        string _gameState = "Main Menu";

        Player _player1;
        Player _player2;

        public struct Player
        {
            public string name;
            public string combatClass;
            public int maxHealth;
            public int health;
            public int level;
            public int damage;
            public int armor;
            public Item item;
        }

        public struct Item
        {
            public string name;
            public string description;
            public string stat;
            public int buff;
        }

        public void Run()
        {
            Start();
            while (!_endGame)
                Update();
            End();
        }

        public void Start()
        {

        }

        public void Update()
        {
            if (_gameState == "Main Menu")
            {
                Console.Clear();
                Console.WriteLine("[Main Menu]\n");
                Console.WriteLine("Welcome to TurnBasedPvPGame!\n");
                Console.WriteLine(" [1] Play");
                Console.WriteLine(" [2] Quit");
                string input = GetStringInput(new[] { "1", "2" });
                if (input == "1")
                    _gameState = "Character Select";
                else if (input == "2")
                    _endGame = true;
            }
            else if (_gameState == "Character Select")
            {
                Console.Clear();
                Console.WriteLine("[Character Select]\n");

                Console.WriteLine("Player 1! Choose your character!");
                Console.WriteLine(" [1] Knight");
                Console.WriteLine(" [2] Wizard");
                string input = GetStringInput(new[] { "1", "2" });
                InitPlayer(ref _player1, input, "Player 1");

                Console.WriteLine();
                Console.WriteLine("A " + _player1.combatClass + "! Good choice!");
                Console.WriteLine();

                Console.WriteLine("Player 2! Choose your character!");
                Console.WriteLine(" [1] Knight");
                Console.WriteLine(" [2] Wizard");
                input = GetStringInput(new[] { "1", "2" });
                InitPlayer(ref _player2, input, "Player 2");

                Console.WriteLine();
                Console.WriteLine(_player2.combatClass + "! Perfect!");
                Console.WriteLine();

                PrintPlayerStats(_player1);
                Console.WriteLine();
                PrintPlayerStats(_player2);
                Console.WriteLine();

                Console.WriteLine("Ready to begin?");
                Console.WriteLine(" [1] Yes");
                Console.WriteLine(" [2] No");
                input = GetStringInput(new[] { "1", "2" });
                if (input == "1")
                    _gameState = "Game";

            }
            else if (_gameState == "Game")
            {
                if (_player1.health > 0 && _player2.health > 0)
                {
                    Console.Clear();
                    Console.WriteLine("[Game]\n");
                    // Do Battle
                    DoBattle();
                }
                else
                {
                    _gameState = "Game Over";
                }
            }

            else if (_gameState == "Game Over")
            {
                Console.Clear();
                Console.WriteLine("[Game Over!]\n");
                if (_player1.health > 0)
                    Console.WriteLine(_player1.name + " wins!");
                else
                    Console.WriteLine(_player1.name + " wins!");

                _gameState = "Main Menu";
                PressAnyKeyToContinue();
            }
        }

        public void DoBattle()
        {
            // Display player stats
            PrintPlayerStats(_player1);
            Console.WriteLine();
            PrintPlayerStats(_player2);
            Console.WriteLine();
            // Player1 turn
            string player1Action = CombatMenu(_player1);
            // Player2 turn
            string player2Action = CombatMenu(_player2);

            ResolveCombat(player1Action, player2Action);
            PressAnyKeyToContinue();
        }


        public void End()
        {
            Console.Clear();
            Console.WriteLine("Thank you for playing TurnBasedPvPGame!");
        }

        #region COMBAT METHODS
        public void AttackPlayer(ref Player player1, ref Player player2)
        {
            // Get damage against armor
            int damage = player1.damage - player2.armor;
            // No negative damage
            player2.health -= Math.Max(0, damage);
            // No negative health
            Math.Clamp(player2.health, 0, player2.maxHealth);
            Console.WriteLine(player1.name + " attacks " + player2.name + " dealing " + damage + " damage!");
        }

        public void ConsumeItem(ref Player player)
        {
            if (player.item.stat == "health")
            {
                player.health += player.item.buff;
                Math.Clamp(player.health, 0, player.maxHealth);
            }
            else if (player.item.stat == "armor")
            {
                player.armor += player.item.buff;
            }
            else
            {
                Console.WriteLine(player.name + " checks their pockets, but there's nothing there!");
                return;
            }
            Console.WriteLine(player.name + " consumes " + player.item.name + ". " + player.item.stat + " +" + player.item.buff);

            player.item = InitItem("None");
        }

        public void ResolveCombat(string player1Action, string player2Action)
        {
            // Player 1 turn
            if (_player1.health > 0)
            {
                // Attack
                if (player1Action == "1")
                {
                    // Deal damage to player2
                    AttackPlayer(ref _player1, ref _player2);
                }

                // Item usage
                if (player1Action == "2")
                {
                    ConsumeItem(ref _player1);
                }
            }
            else
            {
                Console.WriteLine(_player1.name + " is dead!");
            }

            // Player 2 turn
            if (_player2.health > 0)
            {
                // Attack
                if (player2Action == "1")
                {
                    // Deal damage to player1
                    AttackPlayer(ref _player2, ref _player1);
                }

                if (player2Action == "2")
                {
                    ConsumeItem(ref _player2);
                }
            }
            else
            {
                Console.WriteLine(_player2.name + " is dead!");
            }

        }

        public string CombatMenu(Player player)
        {
            Console.WriteLine();
            Console.WriteLine(player.name);
            Console.WriteLine("Select an Action:");
            Console.WriteLine(" [1] Attack");
            Console.WriteLine(" [2] Use Item");
            string input = GetStringInput(new[] { "1", "2" });
            return input;
        }
        #endregion

        #region HELPERS
        public void PrintPlayerStats(Player player)
        {
            Console.WriteLine(player.name + " Level " + player.level + " " + player.combatClass);
            Console.WriteLine(player.health + "/" + player.maxHealth + "hp | " + player.damage + " atk | " + player.armor + " armor");
        }

        // Initialize Player
        public void InitPlayer(ref Player player, string combatClass, string playerName)
        {
            if (combatClass == "1")
            {
                player.name = playerName;
                player.combatClass = "Knight";
                player.maxHealth = 200;
                player.health = player.maxHealth;
                player.level = 1;
                player.damage = 15;
                player.armor = 10;
                player.item = InitItem("Health Potion");
            }
            else
            {
                player.name = playerName;
                player.combatClass = "Wizard";
                player.maxHealth = 100;
                player.health = player.maxHealth;
                player.level = 1;
                player.damage = 50;
                player.armor = 0;
                player.item = InitItem("Armor Potion");
            }
        }

        // Initialize Item
        public Item InitItem(string itemName)
        {
            Item item;
            item.name = itemName;
            if (itemName == "Health Potion")
            {
                item.buff = 20;
                item.stat = "health";
                item.description = "Health potion that heals " + item.buff + " " + item.stat;

            }
            else if (itemName == "Armor Potion")
            {
                item.buff = 5;
                item.stat = "armor";
                item.description = "Armor potion that provides " + item.buff + " " + item.stat;
            }
            else
            {
                item.buff = 0;
                item.stat = "none";
                item.description = "None";
            }
            return item;
        }

        // Self explanatory
        public void PressAnyKeyToContinue()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        // Auto loops for correct input
        public string GetStringInput(string[] validInputs)
        {
            while (true)
            {
                Console.Write(">");
                string input = Console.ReadLine();
                foreach (string item in validInputs)
                {
                    if (input == item)
                    {
                        return input;
                    }
                }
                Console.WriteLine("\nInput not recognized.");
            }
        }
        #endregion
    }
}
