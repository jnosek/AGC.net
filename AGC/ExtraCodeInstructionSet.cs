using Apollo.Virtual.AGC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apollo.Virtual.AGC.Architecture;
using Apollo.Virtual.AGC.Architecture.Instructions;

namespace Apollo.Virtual.AGC
{
    class ExtraCodeInstructionSet : InstructionList
    {
        public ExtraCodeInstructionSet()
            : base(7)
        {
            Add(new BranchZeroToFixed());
            Add(new ExtraQuarterCode2());
            Add(new ExtraQuarterCode3());
            Add(new BranchZeroOrMinusToFixed());
        }
    }
}
