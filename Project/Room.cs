using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Room : IRoom
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; }
        public Dictionary<string, Room> Exits { get; set; }
        public void AddItem(Item item)
        {
            Items.Add(item);
        }
        public void Remove(Item item)
        {
            Items.Remove(item);
        }
        public Room ChangeRoom(string direction)
        {
            if(Exits.ContainsKey(direction))
            {
                return Exits[direction];
            }
            else 
            {
                Console.WriteLine("You shouldn't go that way.");
                Console.WriteLine("Which direction would you like to go?");
                return this;
            }
        }
        public void UseItem(Item item)
        {
            
        }
        public Room(string name, string description) {
            Name = name;
            Description = description;
            Items = new List<Item>();
            Exits = new Dictionary<string, Room>();
        }
    }
}