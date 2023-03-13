using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteGame
{
    internal class Game
    {
        private string field, strategy;
        private int amount;

        public Game(string field, int amount, string strategy) 
        { 
            this.field = field;
            this.amount = amount;
            this.strategy = strategy;
        }
        public string GetField() { return field; }
        public int GetAmount() { return amount; }
        public string GetStrategy() { return strategy; }
    }
}
