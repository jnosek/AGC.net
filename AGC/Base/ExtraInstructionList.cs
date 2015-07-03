using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Base
{
    abstract class ExtraInstructionList : InstructionList, IInstruction
    {
        public ExtraInstructionList(Processor cpu, int count)
            : base(count)
        {
            CPU = cpu;
        }

        public abstract ushort Code { get; }

        public abstract void Execute(ushort K);
    }
}
