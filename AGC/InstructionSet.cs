﻿using Apollo.Virtual.AGC.Core;
using Apollo.Virtual.AGC.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC
{
    class InstructionSet : InstructionList
    {
        public InstructionSet(Processor cpu)
            :base(7)
        {
            CPU = cpu;

            Add(new TransferControl());
            Add(new QuarterCode2(CPU));
            Add(new QuarterCode5(CPU));
            
            Add(new Add());
            Add(new ClearAndAdd());
            Add(new CountCompareAndSkip());
            Add(new ClearAndSubtract());
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
