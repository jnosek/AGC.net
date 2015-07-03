using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Base
{
    public abstract class MemoryAddress
    {
        MemoryBank bank;

        public ushort Address { get; private set; }

        public MemoryAddress(ushort address, MemoryBank bank)
        {
            this.bank = bank;
            this.Address = address;
        }

        protected ushort Get()
        {
            return bank[Address];
        }

        protected void Set(ushort value)
        {
            bank[Address] = value;
        }

        protected void Set(uint value)
        {
            bank[Address] = (ushort)value;
        }

        protected void Set(int value)
        {
            bank[Address] = (ushort)value;
        }
    }
}
