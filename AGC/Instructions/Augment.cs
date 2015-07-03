using Apollo.Virtual.AGC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// AUG - EX 0010 10
    /// Increments a positive value in erasable memory by 1
    /// Or decrements a negative value in erasable memory by -1
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
            var value = CPU.Memory[K];

            // if negative
            if((value & 0x8000) > 0)
            {
                CPU.Memory[K] = value.Add(OnesCompliment.NegativeOne);
            }
            // if positive
            else
            {
                CPU.Memory[K] = value.Add(OnesCompliment.PositiveOne);
            }
        }
    }
}
