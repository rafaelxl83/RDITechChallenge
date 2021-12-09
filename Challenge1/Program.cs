using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// RDI Technical Challenge
/// Made by Rafael Xavier de Lima (rafael.xavier.lima@gmail.com)
/// </summary>
namespace Challenge1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string m, n;
            double d = 0;
            int i = 0;

            if (args.Length < 1)
            {
                Console.WriteLine("Invalid amount!" +
                    "You should enter with two values separetade by a single space!");
                return;
            }

            m = args[0];
            n = args[1];
            if (!double.TryParse(m, out d))
            {
                Console.WriteLine("Invalid amount!");
                return;
            }
            if (!int.TryParse(n, out i))
            {
                Console.WriteLine("Invalid cents amount!");
                return;
            }
            if(i > 99)
            {
                Console.WriteLine("Invalid cents amount!");
                return;
            }

            ConverterAssistant c = new ConverterAssistant();
            if (c.Load())
                Console.WriteLine(c.convertAmount2Words(m, n));
            else
                Console.WriteLine("The config file could not be found!");
            }
    }
}
