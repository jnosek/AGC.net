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
        
        public void WriteAndOverflowCorrect(ushort value)
        {
            uint newValue = value;

            // get lower 14 bits
            uint lowerBits = newValue & 0x3FFF;

            // move 16-th bit, into 15th position, isolate it, and set it in above value;
            newValue = (newValue >> 1 & 0x4000) | lowerBits;

            Write((ushort)newValue);
        }

        public ushort ReadAndSignExtend()
        {
            uint value = Read();

            // take lower 15-bits
            value = value & 0x7FFF;

            // shift left 1 and take 16th bit, combine with lower 15 bits
            value = ((value << 1) & 0x8000) | value;

            return (ushort)value;
        }
    }
}
