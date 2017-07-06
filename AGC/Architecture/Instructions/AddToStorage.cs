using Apollo.Virtual.AGC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Architecture.Instructions
{
    /// <summary>
    /// ADS - 0010 11
    /// QuaterCode Instruction
    /// 
    /// Adds the accumulator to an eraseable memory location and vice versa 
    /// </summary>
    class AddToStorage : IInstruction
    {
        public ushort Code
        {
            get { return 0x03; }
        }

        public Processor CPU { get; set; }

        public void Execute(ushort K)
        {
            var value = CPU.Memory[K];
            CPU.A.Add(value);

            CPU.Memory[K] = CPU.A.Read();
        }
    }
}
