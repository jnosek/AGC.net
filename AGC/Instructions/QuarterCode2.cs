﻿using Apollo.Virtual.AGC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// QC - 0010
    /// 
    /// Operation on 10-bit address space operand restricted to eraseable memory
    /// Followed by 2-bit quarter code for instruction
    /// </summary>
    class QuarterCode2 : ExtraInstructionList
    {
        public QuarterCode2(Processor cpu) : base(cpu, 3)
        {
            Add(new DoubleAddToStorage());
            Add(new AddToStorage());
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