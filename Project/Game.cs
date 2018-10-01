using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project

{
    public class Game : IGame
    {
        bool Playing = true;

        public Room CurrentRoom { get; set; }
        public Player CurrentPlayer { get; set; }
        private Item Ladder;

        public void GetUserInput()
        {
            while (Playing)
            {
                string input = Console.ReadLine().ToLower();
                string input1 = input.Split(" ")[0];
                string input2 = "";
                if (input.Split(" ").Length > 1)
                {
                    input2 = input.Split(" ")[1];
                }

                switch (input)
                {
                    case "help":
                        Help();
                        break;
                    case "look":
                        Look();
                        break;
                    case "take":
                        TakeItem(input2);
                        break;
                    case "use":
                        UseItem(input2);
                        break;
                    case "bag":
                        Bag();
                        break;
                    case "north":
                        CurrentRoom = CurrentRoom.ChangeRoom("north");
                        Look();
                        Console.Write("Which way?");
                        break;
                    case "east":
                        CurrentRoom = CurrentRoom.ChangeRoom("east");
                        Look();
                        Console.Write("Which way?");
                        break;
                    case "west":
                        if (CurrentRoom.Name == "Creepy Basement")
                        {

                        }
                        CurrentRoom = CurrentRoom.ChangeRoom("west");
                        Look();
                        Console.Write("Which way?");
                        break;
                    case "south":
                        CurrentRoom = CurrentRoom.ChangeRoom("south");
                        // EndGame();
                        break;
                    case "quit":
                        Quit();
                        break;
                }
            }
        }

        public void Go(string direction)
        {
            if (CurrentRoom.Exits.ContainsKey(direction))
            {
                CurrentRoom = CurrentRoom.Exits[direction];
            }
            else
            {
                Console.WriteLine("Wrong way");
            }
        }

        public void Help()
        {
            Console.WriteLine("You can type 'North', 'South','East', or 'West' to move");
            Console.WriteLine("You can type 'Look' to see where you are.");
            Console.WriteLine("You can type 'Take' to pick up items.");
            Console.WriteLine("You can type 'Use' to use an item");
            Console.WriteLine("You can type 'Quit' to leave");
            Console.WriteLine("You can type 'Bag' to see what you have");
        }

        public void Bag()
        {
            if (CurrentPlayer.Bag.Count > 0)
            {
                foreach (var item in CurrentPlayer.Bag)
                {
                    Console.WriteLine($"Here's what you have: {item.Name} ");
                    Console.Write("Now what?");
                };
            }
            else
            {
                System.Console.WriteLine("No items yet... ");
            }
        }
        public void Look()
        {
            Console.WriteLine($"{CurrentRoom.Description}");
            if (CurrentRoom.Name == "BigRoom")
            {
                Console.WriteLine("You made it out alive!");
                Console.WriteLine(" You win!");
                Quit();
            }
            else if (CurrentRoom.Name == "Torture Room" && CurrentPlayer.Bag.Count == 0)
            {
                Console.WriteLine("You Wont be able to leave now...");
                Console.WriteLine("You have lost the game.");
            }
        }

        public void Quit()
        {
            Console.Clear();
            Console.WriteLine("You got captured and die");
            Playing = false;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Setup()
        {
            Room dungeon = new Room("dungeon", "You are standing in a dungeon and see a light ahead of you");
            Room scaryroom = new Room("scary room", "You have entered a scary room.");
            Room emptycloset = new Room("Empty closet", "You are in a closet. You must keep moving.");
            Room wetRoom = new Room("Wet room", "everything is wet but you see a window, get out and you survive");

            Ladder = new Item("Ladder", "Ladder to climb");

            scaryroom.AddItem(Ladder);
            CurrentRoom = dungeon;
            CurrentPlayer = new Player();

            dungeon.Exits.Add("north", scaryroom);
            scaryroom.Exits.Add("south", dungeon);
            scaryroom.Exits.Add("east", emptycloset);
            emptycloset.Exits.Add("west", scaryroom);
            emptycloset.Exits.Add("east", wetRoom);
            wetRoom.Exits.Add("west", emptycloset);

        }

        public void StartGame()
        {
            Console.Clear();
            Setup();
            Console.WriteLine("You can type 'North','South','East', or 'West' to move");
            Console.WriteLine("Type 'Help', or type 'Quit' at any point to end the game.");

            Console.WriteLine($"{CurrentRoom.Description}");
            Console.Write("Where to go: ");
            GetUserInput();
        }

        public void TakeItem(string itemName)
        {
            Item item = CurrentRoom.Items.Find(i => i.Name.ToLower().Contains(itemName));
            if (CurrentRoom.Items.Contains(item))
            {
                Console.WriteLine("You picked up the ladder");
                Console.WriteLine("Where now?");
                CurrentPlayer.Bag.Add(item);
                CurrentRoom.Items.Remove(item);
            }
        }

        public void UseItem(string itemName)
        {
            Item item = CurrentPlayer.Bag.Find(i => i.Name.ToLower().Contains(itemName));
            if (item != null)
            {
                if (CurrentRoom.Name == "Wet Room" && itemName == "Ladder")
                {
                    CurrentPlayer.Bag.Remove(item);
                    Console.WriteLine("You use the ladder to jump out the window");
                }
            }
        }
        public void EndGame()
        {
            Console.Write("You tried to go back the way you came and were captured");
            Quit();
        }

        public void Backpack()
        {
            throw new NotImplementedException();
        }
    }
}