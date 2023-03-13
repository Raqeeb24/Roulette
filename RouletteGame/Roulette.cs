using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RouletteGame
{
    public class Roulette
    {
        private const string info = "\n" +
            " ----------------------------------------------------------------\r\n" +
            " | [3] | 6 | [9] | 12 | [15] | 18 | [21] | 24 | [27] | 30 | [33] | 36 | 3/3 |\r\n ----------------------------------------------------------------\r\n" +
            " | [2] | 5 | [8] | 11 | [14] | 17 | [20] | 23 | [26] | 29 | [32] | 35 | 2/3 |\r\n ----------------------------------------------------------------\r\n" +
            " | [1] | 4 | [7] | 10 | [13] | 16 | [19] | 22 | [25] | 28 | [31] | 34 | 1/3 |\r\n ----------------------------------------------------------------\r\n" +
            " |      1 - 12      |      13 - 21     |      22 - 36     |\r\n ----------------------------------------------------------\r\n" +
            " | 0 | gerade | [ungerade] | schwarz | [rot] | hoch | niedrig |\r\n ----------------------------------------------------------";
        private int totalMoney = 1000;
        private string field = "";
        private bool isField = true;
        private int money = 0;
        bool isMoney = true;

        private string strategy = "";
        private bool betAgain = false;
    
        private List<Game> gameBets = new List<Game>();

        private NumberStrategy numberStrategy;
        private TwowayStrategy twowayStrategy;
        private OnethirdStrategy onethirdStrategy;

        private int randomNumber = 0;

        public int GetTotalMoney()
        {
            return totalMoney;
        }
        public void SetTotalMoney(int totalMoney)
        {
            this.totalMoney = totalMoney;
        }
        public void WriteColor()
        {
            var pieces = System.Text.RegularExpressions.Regex.Split(info, @"(\[[^\]]*\])");


            for (int i = 0; i < pieces.Length; i++)
            {
                string piece = pieces[i];
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;


                if (piece.StartsWith("[") && piece.EndsWith("]"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    piece = piece.Substring(1, piece.Length - 2);
                }

                Console.Write(piece);
                Console.ResetColor();
            }

            Console.WriteLine();
        }

        public void SetField(string field)
        {
            this.field = field;
        }

        public bool GetIsField()
        {
            return isField;
        }

        public void SetIsField(bool isField)
        {
            this.isField = isField;
        }

        public bool GetBetAgain()
        {
            return betAgain;
        }

        public void CheckField(string field)
        {
            this.field = field;
            int intField;
            Match match = Regex.Match(field, "[^a-z0-9]", RegexOptions.IgnoreCase);

            if (int.TryParse(field, out intField))
            {
                numberStrategy = new NumberStrategy(this);
                numberStrategy.check(field);
            }
            else if (match.Success)
            {
                onethirdStrategy = new OnethirdStrategy(this);
                onethirdStrategy.check(field);
            }
            else if (field.Length > 2) 
            {
                twowayStrategy = new TwowayStrategy(this);
                twowayStrategy.check(field);
            }
        }

        public bool GetIsMoney()
        {
            return isMoney;
        }
        public void SetIsMoney(bool isMoney)
        {
            this.isMoney = isMoney;
        }

        public void SetStrategy(string strategy)
        {
            this.strategy = strategy;
        }
        public string GetStrategy()
        {
            return strategy;
        }

        public void CheckMoney(int money)
        {
            if (money > totalMoney)
            {
                Console.WriteLine("Ihr Guthaben reicht nicht aus. Setzen Sie einen tieferen Betrag.");
                isMoney = true;
            }
            else
            {
                this.money = money;
                totalMoney -= money;
                isMoney = false;
            }
        }



        public void AddBet(string input)
        {
            gameBets.Add(new Game(field, money, strategy));
            if (input.Equals("ja"))
            {
                if(gameBets.Count < 5 && totalMoney > 0)
                {
                    
                    isField = true;
                    isMoney = true;
                    strategy = "";
                    betAgain = true;
                }
                else
                {
                    betAgain = false;
                }
            }else
            {
                betAgain = false;
            }
        }

        public void Play()
        {
            Random r = new Random();
            randomNumber = r.Next(0, 36);

            Console.WriteLine("Die erdrehte Zahl lautet: " + randomNumber);

            for (int i = 0; i < gameBets.Count; i++)
            {
                string strategy = gameBets[i].GetStrategy();
                string field = gameBets[i].GetField();
                if (strategy.Equals("number")){
                    int multiplier = numberStrategy.result(randomNumber, field);
                    int multiplyamount = multiplier * gameBets[i].GetAmount();
                    if(multiplier > 0)
                    {
                        totalMoney += multiplyamount;
                        Console.WriteLine("Gewonnen: +" + multiplyamount);
                    }
                    else
                    {
                        Console.WriteLine("Verloren!");
                    }
                }else if (strategy.Equals("onethird"))
                {
                    int multiplier = onethirdStrategy.result(randomNumber, field);
                    int multiplyamount = multiplier * gameBets[i].GetAmount();
                    if (multiplier > 0)
                    {
                        totalMoney += multiplyamount;
                        Console.WriteLine("Gewonnen: +" + multiplyamount);
                    }
                    else
                    {
                        Console.WriteLine("Verloren!");
                    }
                }
                else if (strategy.Equals("twoway"))
                {
                    Console.WriteLine("GetField = " + field);
                    int multiplier = twowayStrategy.result(randomNumber, field);
                    int multiplyamount = multiplier * gameBets[i].GetAmount();
                    if (multiplier > 0)
                    {
                        totalMoney += multiplyamount;
                        Console.WriteLine("Gewonnen: +" + multiplyamount);
                    }
                    else
                    {
                        Console.WriteLine("Verloren!");
                    }
                }
                Console.WriteLine("totalbalance now: " +  totalMoney);
            }
        }

        public void Printallbets()
        {
            for (int i = 0;i < gameBets.Count; i++)
            {
                Console.WriteLine("field: " + gameBets[i].GetField());
                Console.WriteLine("amount" + gameBets[i].GetAmount());
                Console.WriteLine("strategy" + gameBets[i].GetStrategy());
            }
        }

        public bool PlayAgain(string input)
        {
            gameBets = new List<Game>();
            isMoney = true;
            return input.Equals("ja") ? true : false;
        }
    }
}
