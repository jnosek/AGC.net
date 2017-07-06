using Apollo.Virtual.AGC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Registers
{
    class ErasableBankRegister : MemoryAddress, IWord
    {
        public ErasableBankRegister(MemoryBank bank) : 
            base(0x03, bank)
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
            // can only set the 3 bits for the erasable memory bank selection
            Set(value & 0x0700);
        }
    }
}
