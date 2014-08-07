using Apollo.Virtual.AGC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// CA - 0011
    /// 
    /// Moves the contents of memory at location K into the accumulator
    /// </summary>
    class ClearAndAdd : IInstruction
    {
        public ushort Code
        {
            get { return 0x3; }
        }

        public Processor CPU { get; set; }

        public void Execute(ushort K)
        {
            var value = CPU.Memory[K];

            // set value in accumulator
            CPU.A.Write(value);

            // value in K is re-written
            CPU.Memory[K] = value;
        }
    }
}
