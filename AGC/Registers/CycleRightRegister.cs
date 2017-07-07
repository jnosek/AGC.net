using Apollo.Virtual.AGC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Registers
{
    class CycleRightRegister : MemoryWord, IWord
    {
        public CycleRightRegister(ushort address, MemoryBank bank)
            : base(address, bank)
        {
        }

        public ushort Read()
        {
            var v = new OnesCompliment(Get());
            v.SignExtend();
            return v;
        }

        public void Write(ushort value)
        {
            // first overflow correct the value
            var v = new OnesCompliment(value);
            v.OverflowCorrect();

            value = v;

            // get bit position 1, and move it to position 15
            var leastSignificateBit = (value & 0x1) << 14;

            // 15 bit register, so shift right 1, get the lower 14 bits, and add the wrap around bit
            var bits = ((value >> 1) & 0x3FFF) | leastSignificateBit;

            // write the cycled value into memory
            Set(bits);
        }
    }
}