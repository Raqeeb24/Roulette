using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteGame
{
    interface Strategy
    {
        int result(int randomNumber, string field);
        void check(string input);
    }
}
