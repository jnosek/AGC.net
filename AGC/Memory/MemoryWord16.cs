using Apollo.Virtual.AGC.Math;

namespace Apollo.Virtual.AGC.Memory
{
    /// <summary>
    /// Full 16-bit memory word, for special registers
    /// Does not overflow correct
    /// </summary>
    class MemoryWord16 : MemoryWord
    {
        public MemoryWord16(ushort address, MemoryBank bank)
            : base(address, bank)
        {
        }

        /// <summary>
        /// Write the full 16 bits into memory without overflow correction
        /// </summary>
        /// <param name="value"></param>
        public override void Write(OnesCompliment value)
        {
            WriteRaw(value.NativeValue);
        }
    }
}
