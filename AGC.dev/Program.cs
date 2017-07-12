using Apollo.Virtual.AGC;
using System;

namespace AGC.dev
{
    class Program
    {
        static void Main(string[] args)
        {
            var computer = new Computer();
            var memory = computer.Memory;

            memory[0x800] = (0x6301).ToOnesCompliment();
            memory[0x801] = (0x02).ToOnesCompliment();
            computer.Start();

            Console.WriteLine("A: {0}", memory[0x0]);

            Console.ReadKey();
        }
    }
}
