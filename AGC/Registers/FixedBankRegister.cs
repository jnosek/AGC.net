﻿using Apollo.Virtual.AGC.Math;
using Apollo.Virtual.AGC.Memory;

namespace Apollo.Virtual.AGC.Registers
{
    class FixedBankRegister : MemoryWord
    {
        public FixedBankRegister(ushort address, MemoryBank bank) 
            : base(address, bank)
        {
        }

        public override void Write(ushort value)
        {
            var maskedValue = value & 0x7C00;

            // read current value of bb register
            // use mask to get EB value
            var eb = Read(0x6) & 0x0007;

            // write new bb value
            UnmodifiedWrite(maskedValue | eb, 0x6);

            // can only set the 5 bits for the fixed memory bank selection
            UnmodifiedWrite(maskedValue);
        }
    }
}
