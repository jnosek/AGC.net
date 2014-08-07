using Apollo.Virtual.AGC.Base;
using Apollo.Virtual.AGC.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC
{
    class InstructionSet : InstructionList
    {
        private Processor CPU;
        
        public InstructionSet(Processor cpu)
            :base(7)
        {
            CPU = cpu;

            Add(new TransferControl { CPU = CPU });
            Add(new QuarterCode2(CPU));
            Add(new ClearAndAdd { CPU = CPU });
            Add(new QuarterCode5(CPU));
            Add(new Add { CPU = CPU });
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
