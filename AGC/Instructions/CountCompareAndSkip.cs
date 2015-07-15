using Apollo.Virtual.AGC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// CCS - 0001
    /// 
    /// The "Count, Compare, and Skip" instruction stores a variable from erasable memory into the accumulator 
    /// (which is decremented), and then performs one of several jumps based on the original value of the variable.
    class CountCompareAndSkip : IInstruction
    {
        public ushort Code
        {
            get { return 0x01; }
        }

        public Processor CPU { get; set; }

        public void Execute(ushort K)
        {
            // retrieve value from memory
            var value = new SinglePrecision(CPU.Memory[K]);

            // 1) compute the Diminished ABSolute value found at K and set in A
 
            // if negative, NOT 1's to get ABS
            var abs = value.IsNegative ? ~value : value;

            if (abs > 1)
                CPU.A.Write(abs + OnesCompliment.NegativeOne);
            else
                CPU.A.Write(0);

            // 2) K is edited
            CPU.Memory[K] = value;

            // 3) branch upon original value of K

            // if greater than +0 we do nothing, continue to next instruction as usual

            // if == +0 increment by 1
            if (value.IsPositiveZero)
                CPU.Z.Increment();
            // if == -0 increment by 3
            else if(value.IsNegativeZero)
            {
                CPU.Z.Increment();
                CPU.Z.Increment();
                CPU.Z.Increment();
            }
            // if < 0 increment by 2
            else if (value.IsNegative)
            {
                CPU.Z.Increment();
                CPU.Z.Increment();
            }
        }
    }
}
