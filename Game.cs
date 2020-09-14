using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace TurnBasedPvPGame
{
    public struct Item
    {
        public string name;
        public string description;
        public int buff;
    }

    class Game
    {
        bool _endGame = false;
        string _gameState = "Main Menu";

        Player _player1;
        Player _player2;


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
                Console.WriteLine("A " + _player1.GetCombatClass() + "! Good choice!");
                Console.WriteLine();

                Console.WriteLine("Player 2! Choose your character!");
                Console.WriteLine(" [1] Knight");
                Console.WriteLine(" [2] Wizard");
                input = GetStringInput(new[] { "1", "2" });
                InitPlayer(ref _player2, input, "Player 2");

                Console.WriteLine();
                Console.WriteLine(_player2.GetCombatClass() + "! Perfect!");
                Console.WriteLine();

                _player1.PrintStats();
                Console.WriteLine();
                _player2.PrintStats();
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
                if (_player1.IsAlive() && _player2.IsAlive())
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
                if (_player1.IsAlive())
                    Console.WriteLine(_player1.GetName() + " wins!");
                else
                    Console.WriteLine(_player1.GetName() + " wins!");

                _gameState = "Main Menu";
                PressAnyKeyToContinue();
            }
        }

        public void DoBattle()
        {
            // Display player stats
            _player1.PrintStats();
            Console.WriteLine();
            _player2.PrintStats();
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
        public void ResolveCombat(string player1Action, string player2Action)
        {
            // Player 1 turn
            if (_player1.IsAlive())
            {
                // Attack
                if (player1Action == "1")
                {
                    // Deal damage to player2
                    _player1.Attack(_player2);
                }

                // Item usage
                if (player1Action == "2")
                {
                    _player1.ConsumeItem();
                }
            }
            else
            {
                Console.WriteLine(_player1.GetName() + " is dead!");
            }

            // Player 2 turn
            if (_player2.IsAlive())
            {
                // Attack
                if (player2Action == "1")
                {
                    // Deal damage to player1
                    _player2.Attack(_player1);
                }

                if (player2Action == "2")
                {
                    _player2.ConsumeItem();
                }
            }
            else
            {
                Console.WriteLine(_player2.GetName() + " is dead!");
            }

        }

        public string CombatMenu(Player player)
        {
            Console.WriteLine();
            Console.WriteLine(player.GetName());
            Console.WriteLine("Select an Action:");
            Console.WriteLine(" [1] Attack");
            Console.WriteLine(" [2] Use Item");
            string input = GetStringInput(new[] { "1", "2" });
            return input;
        }
        #endregion

        #region HELPERS
        // Initialize Player
        public void InitPlayer(ref Player player, string combatClass, string playerName)
        {
            if (combatClass == "1")
            {
                player = new Player(playerName, "Knight", 200, 1, 15, 10, InitItem("Health Potion"));
            }
            else
            {
                player = new Player(playerName, "Wizard", 100, 1, 50, 0, InitItem("Armor Potion"));
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
                item.description = "Health potion that heals " + item.buff + " Health";

            }
            else if (itemName == "Armor Potion")
            {
                item.buff = 5;
                item.description = "Armor potion that provides " + item.buff + " Armor";
            }
            else
            {
                item.buff = 0;
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
