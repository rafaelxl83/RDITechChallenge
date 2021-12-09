using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/// <summary>
/// RDI Technical Challenge
/// Made by Rafael Xavier de Lima (rafael.xavier.lima@gmail.com)
/// </summary>
namespace Challenge2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String errMessage = 
                "Invalid path!" + Environment.NewLine +
                "Only directions command are accepted:" + Environment.NewLine +
                " R: right" + Environment.NewLine +
                " L: left" + Environment.NewLine +
                " U: up" + Environment.NewLine +
                " D: down" + Environment.NewLine +
                "Please, try again!";

            if (args.Length < 1)
            {
                Console.WriteLine(errMessage);
                return;
            }

            if(Regex.Matches(args[0], "[^RLUD]", RegexOptions.Singleline).Count > 0)
            {
                Console.WriteLine(errMessage);
                return;
            }

            Robot robot = new Robot();
            Console.WriteLine(robot.getLastLoop(args[0]));
        }
    }
}
