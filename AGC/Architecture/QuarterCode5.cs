using Apollo.Virtual.AGC.Architecture.Instructions;
using Apollo.Virtual.AGC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Architecture
{
    /// <summary>
    /// QC - 0101
    /// 
    /// Operation on 10-bit address space operand restricted to eraseable memory
    /// Followed by 2-bit quarter code for instruction
    /// </summary>
    class QuarterCode5 : QuarterCodeInstructionList
    {
        public QuarterCode5()
        {
            Add(new TransferToStorage());
        }

        public override ushort Code
        {
            get { return 0x05; }
        }
    }
}
