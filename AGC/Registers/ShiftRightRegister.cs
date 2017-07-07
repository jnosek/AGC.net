using Apollo.Virtual.AGC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Registers
{
    class ShiftRightRegister : MemoryWord, IWord
    {
        public ShiftRightRegister(ushort address, MemoryBank bank)
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

            // preserve MSB and OR with shifted bits
            var bits = (value & 0x4000) | (value >> 1);

            // write the shifted value into memory
            Set(bits);
        }
    }
}
