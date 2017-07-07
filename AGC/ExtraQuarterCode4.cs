using Apollo.Virtual.AGC.Architecture;
using Apollo.Virtual.AGC.Architecture.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC
{
    class ExtraQuarterCode4 : QuarterCodeInstructionList
    {
        public ExtraQuarterCode4()
        {
            Add(new DoubleClearAndSubtract());
        }

        public override ushort Code
        {
            get { return 0x04; }
        }
    }
}
