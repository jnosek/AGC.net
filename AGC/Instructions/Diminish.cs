﻿namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// DIM - EX 0010 11
    /// Decrements a positive non-zero value in an erasable-memory location in-place,
    /// Or increments a negative non-zero value.
    /// </summary>
    class Diminish : IInstruction
    {
        public Diminish(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        public ushort Code => 0x00_3;

        public void Execute(ushort K)
        {
            var value = cpu.Memory[K];

            // if negative
            if(value.IsNegative)
            {
                cpu.Memory[K] = value + OnesCompliment.PositiveOne;
            }
            // is positive
            else
            {
                cpu.Memory[K] = value + OnesCompliment.NegativeOne;
            }
        }
    }
}
