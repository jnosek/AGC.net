using Apollo.Virtual.AGC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Registers
{
    class Accumulator : FullRegister
    {
        public void Add(ushort value)
        {
            uint sum = SinglePrecision.Add(Read(), value);

            // if we have overflow, most likely from subtracting negative numbers
            if((sum & 0x10000) > 0)
            {
                // we need to Single Precision correct the negative number by adding 1 and taking the lower 16 bits
                sum = sum + 1;
                sum = sum & 0xFFFF;
            }

            this.Write((ushort)sum);
        }
    }
}
