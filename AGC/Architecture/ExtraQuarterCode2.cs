using Apollo.Virtual.AGC.Architecture.Instructions;
using Apollo.Virtual.AGC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Architecture
{
    /// <summary>
    /// EQC - EX 0010
    /// 
    /// 10-bit memory instructions in the extra code set
    /// </summary>
    class ExtraQuarterCode2 : QuarterCodeInstructionList
    {
        public ExtraQuarterCode2()
        {
            Add(new Augment());
        }

        public override ushort Code
        {
            get { return 0x02; }
        }
    }
}
