using Apollo.Virtual.AGC.Memory;

namespace Apollo.Virtual.AGC.Registers
{
    class ErasableBankRegister : MemoryWord
    {
        public ErasableBankRegister(ushort address, MemoryBank bank)
            : base(address, bank)
        {
        }

        public override void Write(OnesCompliment value)
        {
            var maskedValue = value & 0x0700;
            // can only set the 3 bits for the erasable memory bank selection
            WriteRaw(maskedValue);
        }
    }
}
