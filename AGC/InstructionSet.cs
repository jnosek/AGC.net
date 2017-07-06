using Apollo.Virtual.AGC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apollo.Virtual.AGC.Architecture;
using Apollo.Virtual.AGC.Architecture.Instructions;

namespace Apollo.Virtual.AGC
{
    class InstructionSet : InstructionList
    {
        public InstructionSet()
            :base(7)
        {

            Add(new TransferControl());
            Add(new QuarterCode2());
            Add(new QuarterCode5());
            
            Add(new Add());
            Add(new ClearAndAdd());
            Add(new CountCompareAndSkip());
            Add(new ClearAndSubtract());
        }
    }
}
