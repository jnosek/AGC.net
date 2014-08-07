using Apollo.Virtual.AGC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// QC - 0101
    /// 
    /// Operation on 10-bit address space operand restricted to eraseable memory
    /// Followed by 2-bit quarter code for instruction
    /// </summary>
    class QuarterCode5 : InstructionList, IInstruction
    {
        public QuarterCode5(Processor CPU) : base (3)
        {
            this.CPU = CPU;

            Add(new TransferToStorage { CPU = CPU });
        }

        public ushort Code
        {
            get { return 0x05; }
        }

        public Processor CPU { get; set; }

        public void Execute(ushort K)
        {
            var quarterCode = (ushort)(K >> 10);
            K = (ushort)(K & 0x3FF);

            base[quarterCode].Execute(K);
        }
    }
}
