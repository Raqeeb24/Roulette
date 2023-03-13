using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteGame
{
    public class TwowayStrategy : Strategy
    {
        private Roulette roulette;

        public TwowayStrategy(Roulette roulette)
        {
            this.roulette = roulette;
        }
        public void check(string input)
        {
            roulette.SetIsField(false);
            roulette.SetStrategy("twoway");
            switch (input)
            {
                case "gerade":
                    Console.WriteLine("gerade");
                    break;
                case "ungerade":
                    Console.WriteLine("ungerade");
                    break;
                case "schwarz":
                    Console.WriteLine("schwarz");
                    break;
                case "rot":
                    Console.WriteLine("rot");
                    break;
                case "hoch":
                    Console.WriteLine("hoch");
                    break;
                case "niedrig":
                    Console.WriteLine("niedrig");
                    break;
                default:
                    Console.WriteLine("not correct");
                    roulette.SetIsField(true);
                    roulette.SetStrategy("");
                    break;
            }
        }

        public int result(int randomNumber, string field)
        {
            Console.WriteLine("result of twoway");
            string strategy = roulette.GetStrategy();

            bool even = randomNumber % 2 == 0;
            bool higher = randomNumber >= 18;

            switch (field)
            {
                case "gerade":
                    return even ?  2 : 0;
                    break;
                case "ungerade":
                    return even ? 2 : 0;
                    break;
                case "schwarz":
                    if(randomNumber == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return even ? 2 : 0;
                    }
                    break;
                case "rot":
                    return even ? 2 : 0;
                    break;
                case "hoch":
                    return higher ? 2 : 0;
                    break;
                case "niedrig":
                    return higher ? 2 : 0;
                    break;
                default:
                    return 0;
                    break;
            }

        }
    }
}
