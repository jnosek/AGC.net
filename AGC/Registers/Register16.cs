using Apollo.Virtual.AGC.Memory;

namespace Apollo.Virtual.AGC.Registers
{
    /// <summary>
    /// Registerd backed by a full 16-bit memory word
    /// </summary>
    class Register16 : MemoryWord16
    {
        public Register16(ushort address, MemoryBank bank)
            : base(address, bank)
        {
        }
    }
}
