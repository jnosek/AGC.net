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
    class Register
    {
        private Memory ram;
        private uint address;

        public Register(Memory ram, uint address)
        {
            this.ram = ram;
            this.address = address;
        }

        public virtual void Write(ushort value)
        {
            ram[address] = value;
        }

        public virtual ushort Read()
        {
            return ram[address];
        }
    }
}
