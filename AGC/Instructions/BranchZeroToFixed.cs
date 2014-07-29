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
    /// Jumps to a memory location in the fixed bank if the accumulator is 0
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
            if (CPU.A.Read() == 0)
                CPU.Z.Write(K);
        }
    }
}
