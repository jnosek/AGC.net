using Apollo.Virtual.AGC.Memory;

namespace Apollo.Virtual.AGC.Registers
{
    /// <summary>
    /// 12-bit register for the address of the next instruction
    /// </summary>
    class ProgramCounter: MemoryWord
    {
        public ProgramCounter(ushort address, MemoryBank bank)
            : base(address, bank)
        {
        }

        public void Increment()
        {
            var value = Read();

            // increment and write
            Write(value + OnesCompliment.PositiveOne);
        }

        public override void Write(OnesCompliment value)
        {
            // only store lower 12 bits
            var maskedBits = value & 0xFFF;

            WriteRaw(maskedBits);
        }
    }
}
