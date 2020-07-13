using Apollo.Virtual.AGC.Math;
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
            Write(OnesCompliment.AddPositiveOne(value));
        }

        public override void Write(ushort value)
        {
            // only store lower 12 bits
            var maskedBits = value & 0xFFF;

            UnmodifiedWrite(maskedBits);
        }
    }
}
