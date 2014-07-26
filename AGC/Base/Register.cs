using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Base
{
    /// <summary>
    /// 16 bit register
    /// 15 data bits, 1 odd-parity bit
    /// 
    /// All registers are memory mapped
    /// </summary>
    public class Register
    {
        private MemoryAddress address;

        public Register(MemoryAddress address)
        {
            this.address = address;
        }

        public virtual void Write(ushort value)
        {
            address.Write(value);
        }

        public virtual ushort Read()
        {
            return address.Read();
        }

        public void Add(ushort K)
        {
            this.Write((ushort)(this.Read() + K));
        }
    }
}
