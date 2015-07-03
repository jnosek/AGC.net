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
    class ExtraQuarterCode: ExtraInstructionList
    {
        public ExtraQuarterCode(Processor cpu)
            : base(cpu, 3)
        {
            Add(new Augment());
        }

        public override ushort Code
        {
            get { return 0x02; }
        }

        public override void Execute(ushort K)
        {
            var quarterCode = (ushort)(K >> 10);
            K = (ushort)(K & 0x3FF);

            base[quarterCode].Execute(K);
        }
    }
}
