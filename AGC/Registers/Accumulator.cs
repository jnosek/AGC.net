using Apollo.Virtual.AGC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Registers
{
    class Accumulator : FullRegister
    {
        public Accumulator(MemoryBank bank)
            : base(0x0, bank)
        {
        }

        public void Add(ushort value)
        {
            var sum = new SinglePrecision(value) + Get();

            Set(sum);
        }

        public bool IsOverflow
        {
            get
            {
                // look at bits 16 and 15 to see if they are different
                var value = Get() & 0xC000;

                return value == 0x8000 || value == 0x4000;
            }
        }
    }
}
