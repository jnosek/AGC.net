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
            var sum = Read() + value;

            this.Write((ushort)sum);
        }
    }
}
