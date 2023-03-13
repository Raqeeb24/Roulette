using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouletteGame;

namespace RouletteGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Roulette roulette = new Roulette();
            Console.WriteLine("Das Anfangsguthaben beträgt: " +  roulette.GetTotalMoney());
            do
            {
                do
                {
                    string field = "";

                    do
                    {
                        Console.WriteLine("Dies sind die möglichen Felder, auf denen man setzen kann");
                        roulette.WriteColor();
                        Console.WriteLine("Einfach das gewünschte Feld eingeben um zu spielen\nz.B. '13-21' oder 'niedrig'");

                        roulette.CheckField(Console.ReadLine());
                    } while (roulette.GetIsField());

                    Console.WriteLine("Feld: " + field);

                    while (roulette.GetIsMoney())
                    {
                        Console.WriteLine("Setze einen Betrag auf das Feld");
                        try
                        {
                            roulette.CheckMoney(Convert.ToInt32(Console.ReadLine()));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{ex.Message}");
                            roulette.SetIsMoney(true);
                        }
                    }
                    Console.WriteLine("Noch verfügbares Guthaben: " + roulette.GetTotalMoney());
                    Console.WriteLine();
                    Console.WriteLine("Möchten Sie auf eine weiteres Feld eine Wette setzen? ja/nein");
                    roulette.AddBet(Console.ReadLine());
                } while (roulette.GetBetAgain());


                //roulette.Printallbets();

                roulette.Play();

                Console.WriteLine("Möchten Sie wieder spielen? ja/nein");

            } while (roulette.PlayAgain(Console.ReadLine()));

        }
    }
}
