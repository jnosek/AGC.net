using Apollo.Virtual.AGC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// AD - 0110
    /// 
    /// Adds the value located in K to the accumulator
    /// </summary>
    class Add : IInstruction
    {
        public Processor CPU { get; set; }

        public ushort Code { get { return 0x06; } }

        public void Execute(ushort K)
        {
            var value = CPU.Memory[K];
            CPU.A.Add(CPU.Memory[K]);

            // value in K is re-written
            CPU.Memory[K] = value;
        }
    }
}
