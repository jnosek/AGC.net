using Apollo.Virtual.AGC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// CS - 0100
    /// 
    /// The "Clear and Subtract" instruction moves the 1's-complement (i.e., the negative) of a memory location into the accumulator.
    /// 
    /// Also:
    /// COM - 0100 0000 0000 0000
    /// The "Complement the Contents of A" bitwise complements the accumulator
    /// Assembles as CS A
    /// </summary>
    class ClearAndSubtract : IInstruction
    {
        public ushort Code
        {
            get { return 0x4; }
        }

        public Processor CPU { get; set; }

        public void Execute(ushort K)
        {
            var value = CPU.Memory[K];

            // write the compliment to the accumulator
            CPU.A.Write((ushort)~value);

            // if not the A register, re-write value to K
            if (K != CPU.A.Address)
                CPU.Memory[K] = value;
        }
    }
}
