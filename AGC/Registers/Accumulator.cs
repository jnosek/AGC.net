using Apollo.Virtual.AGC.Base;
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
            uint sum = Get().Add(value);

            // if we have overflow, most likely from subtracting negative numbers
            if((sum & 0x10000) > 0)
            {
                // we need to Single Precision correct the negative number by adding 1 and taking the lower 16 bits
                sum = sum + 1;
                sum = sum & 0xFFFF;
            }

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
