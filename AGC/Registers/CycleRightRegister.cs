using Apollo.Virtual.AGC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Registers
{
    class CycleRightRegister : IWord, IRegister
    {
        MemoryAddress memory;

        public ushort Address
        {
            get { return memory.Address; }
        }

        public ushort Read()
        {
            return memory.ReadAndSignExtend();
        }

        public void Write(ushort value)
        {
            // first overflow correct the value
            memory.WriteAndOverflowCorrect(value);

            // retreive value
            value = memory.Read();
            
            // get bit position 1, and move it to position 15
            var leastSignificateBit = (value & 0x1) << 14;

            // 15 bit register, so shift right 1, get the lower 14 bits, and add the wrap around bit
            var bits = ((value >> 1) & 0x3FFF) | leastSignificateBit;

            // write the cycled value into memory
            memory.Write((ushort) bits);
        }

        MemoryAddress IRegister.Memory
        {
            set { this.memory = value; }
        }
    }
}