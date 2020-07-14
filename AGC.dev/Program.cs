using Apollo.Virtual.AGC;
using Apollo.Virtual.AGC.Math;
using System;

namespace AGC.dev
{
    class Program
    {
        static void Main(string[] args)
        {
            var computer = new Computer();
            var memory = computer.Memory;

            memory[0x800] = 0x6301;
            memory[0x801] = 0x02;
            computer.Start();

            Console.WriteLine("A: {0}", memory[0x0]);

            Console.ReadKey();
        }
    }
}
