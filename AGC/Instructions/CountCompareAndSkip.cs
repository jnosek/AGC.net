﻿using Apollo.Virtual.AGC.Math;

namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// CCS - 0001
    /// 
    /// The "Count, Compare, and Skip" instruction stores a variable from erasable memory into the accumulator 
    /// (which is decremented), and then performs one of several jumps based on the original value of the variable.
    public class CountCompareAndSkip : IInstruction
    {
        public CountCompareAndSkip(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        public ushort Code => 0x1;

        public void Execute(ushort K)
        {
            // retrieve value from memory
            var value = cpu.Memory[K];

            // 1) compute the Diminished ABSolute value found at K and set in A

            var dabs = value.GetDiminishedAbsoluteValue();


            // 2) K is edited, then write dabs to A
            // we do it in this order in case K is the A register (which is common for loops)
            // that way if K is an editing register, 
            // it is adjust accordingly, 
            // but the computed value is A is preserved
            cpu.Memory[K] = value;
            cpu.A.Write(dabs);

            // 3) branch upon original value of K

            // if greater than +0 we do nothing, continue to next instruction as usual

            // if == +0 increment by 1
            if (value.IsPositiveZero)
                cpu.Z.Increment();
            // if == -0 increment by 3
            else if(value.IsNegativeZero)
            {
                cpu.Z.Increment();
                cpu.Z.Increment();
                cpu.Z.Increment();
            }
            // if < 0 increment by 2
            else if (value.IsNegative)
            {
                cpu.Z.Increment();
                cpu.Z.Increment();
            }
        }
    }
}
