using Apollo.Virtual.AGC.Memory;

namespace Apollo.Virtual.AGC.Registers
{
    class FullRegister : MemoryWord16
    {
        public FullRegister(ushort address, MemoryBank bank)
            : base(address, bank)
        {
        }
    }
}
