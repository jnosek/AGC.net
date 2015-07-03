using Apollo.Virtual.AGC.Base;
using Apollo.Virtual.AGC.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC
{
    class ExtraCodeInstructionSet : InstructionList
    {
        public ExtraCodeInstructionSet(Processor cpu)
            : base(7)
        {
            CPU = cpu;

            Add(new BranchZeroToFixed());
            Add(new ExtraQuarterCode(CPU));
            Add(new BranchZeroOrMinusToFixed());
        }

        public new IInstruction this[ushort code]
        {
            get
            {
                return base[code];
            }
        }
    }
}
