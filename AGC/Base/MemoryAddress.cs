using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Base
{
    public class MemoryAddress
    {
        private MemoryBank m;
        private uint address;

        public MemoryAddress(MemoryBank memory, uint address)
        {
            this.m = memory;
            this.address = address;
        }

        public ushort Read()
        {
            return m[address];
        }

        public void Write(ushort value)
        {
            m[address] = value;
        }
    }
}
