using System;
using wayOfLifeEngine;

namespace wayOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            var space = new Space(120, 35, 2);
            while (true)
            {
                Console.Clear();
                Console.WriteLine(space.ToString());
                //Console.ReadLine();
                space.Cycle();
            }
        }
    }
}
