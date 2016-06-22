using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random ran = new Random();
            if (ran.Next(0, 100) < 50)
            {
                Console.WriteLine("Mate");
            }
            else
            {
                Console.WriteLine("Cafe");
            }

            while (true) ;
        }
    }
}
