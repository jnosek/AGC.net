using Apollo.Virtual.AGC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// BZF - EX 0001
    /// 
    /// Jumps to a fixed memory location if the accumulator is 0
    /// </summary>
    class BranchZeroToFixed : IInstruction
    {
        public ushort Code
        {
            get { return 0x01; }
        }

        public Processor CPU { get; set; }

        public void Execute(ushort K)
        {
            // if in overflow, no jump
            if(CPU.A.IsOverflow)
                return;
            
            var value = CPU.A.Read();

            // if +0 or -0, then jump
            if (value == 0 || value == OnesCompliment.NegativeZero)
                CPU.Z.Write(K);
        }
    }
}
