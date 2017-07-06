using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Architecture
{
    abstract class QuarterCodeInstructionList : InstructionList, IInstruction
    {
        public QuarterCodeInstructionList()
            : base(4)
        {
        }

        public abstract ushort Code { get; }

        public Processor CPU { get; set; }

        public void Execute(ushort K)
        {
            var quarterCode = (ushort)(K >> 10);
            K = (ushort)(K & 0x3FF);

            // update CPU reference
            base[quarterCode].CPU = CPU;
            base[quarterCode].Execute(K);
        }
    }
}
