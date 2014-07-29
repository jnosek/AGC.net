using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Base
{
    public class MemoryAddress
    {
        private MemoryBank m;
        private ushort index;

        public ushort Address { get; private set; }

        public MemoryAddress(MemoryBank memory, ushort address, ushort index)
        {
            this.m = memory;
            this.index = index;
            this.Address = address;
        }

        public ushort Read()
        {
            return m[index];
        }

        public void Write(ushort value)
        {
            m[index] = value;
        }
    }
}
