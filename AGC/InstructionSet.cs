using Apollo.Virtual.AGC.Base;
using Apollo.Virtual.AGC.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC
{
    class InstructionSet
    {
        private Processor CPU;
        private InstructionList instructions;
        
        public InstructionSet(Processor cpu)
        {
            CPU = cpu;
            instructions = new InstructionList(7);

            instructions.Add(new QuarterCode { CPU = CPU });
            instructions.Add(new Add { CPU = CPU });
        }

        public IInstruction this[ushort code]
        {
            get
            {
                return instructions[code];
            }
        }
    }
}
