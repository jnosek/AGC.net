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
        private Processor CPU;

        public ExtraCodeInstructionSet(Processor cpu)
            : base(7)
        {
            CPU = cpu;

            Add(new BranchZeroToFixed { CPU = CPU });
            Add(new ExtraQuarterCode { CPU = CPU });
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
