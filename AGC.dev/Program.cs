using Apollo.Virtual.AGC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGC.dev
{
    class Program
    {
        static void Main(string[] args)
        {
            var computer = new Computer();

            computer.Memory.GetAddress(0x300).Write(0x6301);
            computer.Memory.GetAddress(0x301).Write(0x02);
            computer.CPU.Z.Write(0x300);
            computer.CPU.Execute();

            Console.WriteLine("A: {0}", computer.CPU.A.Read());

            Console.ReadKey();
        }
    }
}
