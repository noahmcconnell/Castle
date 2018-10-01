using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Player : IPlayer
    {
        public string PlayerName { get; set; }
        public string Name { get; }
        public List<Item> Bag { get; set; }
        List<Item> IPlayer.Bag { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public Player(string name)
        {
            Name = name;
            Bag = new List<Item>();
        }

        public Player()
        {
        }

    }
}