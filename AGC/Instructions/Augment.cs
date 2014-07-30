using Apollo.Virtual.AGC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// AUG - EX 0010 10
    /// </summary>
    class Augment: IInstruction
    {
        public ushort Code
        {
            get { return 0x02; }
        }

        public Processor CPU { get; set; }

        public void Execute(ushort K)
        {
            var value = 
                // if positive, add 1
                (K & 0x4000) == 0 ? (CPU.Memory[K] + 1) :
                // else negative, subtract 1
                (CPU.Memory[K] - 1);

            CPU.Memory[K] = (ushort)value;
        }
    }
}
