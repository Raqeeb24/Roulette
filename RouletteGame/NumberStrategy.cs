using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteGame
{
    public class NumberStrategy : Strategy
    {
        private Roulette roulette;

        public NumberStrategy(Roulette roulette) { 
            this.roulette = roulette;
        }
        public void check(string input)
        {
            int intField = Convert.ToInt32(input);
            if (intField >= 0 && intField <= 36)
            {
                roulette.SetIsField(false);
                Console.WriteLine(intField.ToString());
                roulette.SetStrategy("number");
            }
            else
            {
                roulette.SetIsField(true);
                Console.WriteLine("not valid number");
            }
        }
        public int result(int randomNumber, string field)
        {
            int intField = Convert.ToInt32(field);
            Console.WriteLine("result of number");
            if (intField == randomNumber)
            {
                return 36;
            }
            else
            {
                return 0;
            }
        }
    }
}
