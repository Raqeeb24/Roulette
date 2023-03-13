using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteGame
{   
    public class OnethirdStrategy : Strategy
    {
        private Roulette roulette;

        public OnethirdStrategy(Roulette roulette)
        {
            this.roulette = roulette;
        }
        public void check(string input)
        {
            roulette.SetIsField(false);
            roulette.SetStrategy("onethird");
            switch (input)
            {
                case "1-12":
                    Console.WriteLine("1-12");
                    break;
                case "13-21":
                    Console.WriteLine("13-21");
                    break;
                case "22-36":
                    Console.WriteLine("22-36");
                    break;
                case "!info":
                    roulette.WriteColor();
                    Console.WriteLine();
                    break;
                default:
                    Console.WriteLine("not correct too");
                    roulette.SetIsField(true);
                    roulette.SetStrategy("");
                    break;
            }
        }
        public int result(int randomNumber, string field)
        {
            Console.WriteLine("result of onethird");
            switch (field)
            {
                case "1-12":
                    return randomNumber > 0 && randomNumber <= 12 ? 3 : 0;
                    break;
                case "13-21":
                    return randomNumber > 12 && randomNumber <= 21 ? 3 : 0;
                    break;
                case "22-36":
                    return randomNumber > 21 && randomNumber <= 36 ? 3 : 0;
                    break;
                default:
                    return 0;
                    break;

            }
        }
    }
}
