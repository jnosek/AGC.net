using Apollo.Virtual.AGC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// EQC - EX 0010
    /// 10-bit memroy instructions in the extra code set
    /// </summary>
    class ExtraQuarterCode: InstructionList, IInstruction
    {
        public ExtraQuarterCode(Processor CPU)
            : base(3)
        {
            this.CPU = CPU;
            Add(new Augment { CPU = this.CPU });
        }

        public ushort Code
        {
            get { return 0x02; }
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
