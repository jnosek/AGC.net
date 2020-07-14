using Apollo.Virtual.AGC.Math;
using Apollo.Virtual.AGC.Memory;

namespace Apollo.Virtual.AGC.Registers
{
    class ErasableBankRegister : MemoryWord
    {
        public ErasableBankRegister(ushort address, MemoryBank bank)
            : base(address, bank)
        {
        }

        public override void Write(ushort value)
        {
            var maskedValue = value & 0x0700;

            // read current value of bb register
            // use mask to get FB value
            var fb = Read(0x6) & 0x7C00;

            // write new bb value (fb and right shifted eb value)
            UnmodifiedWrite(maskedValue >> 8 | fb, 0x6);

            // can only set the 3 bits for the erasable memory bank selection
            UnmodifiedWrite(maskedValue);
        }
    }
}
