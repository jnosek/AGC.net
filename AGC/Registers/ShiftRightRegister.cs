using Apollo.Virtual.AGC.Math;
using Apollo.Virtual.AGC.Memory;

namespace Apollo.Virtual.AGC.Registers
{
    class ShiftRightRegister : MemoryWord
    {
        public ShiftRightRegister(ushort address, MemoryBank bank)
            : base(address, bank)
        {
        }

        public override void Write(ushort value)
        {
            // in case this is coming from a 16-bit register, overflow correct it, but we only want the 15 bit value
            var correctedValue = OverflowCorrect(value) & 0x7FFF;

            // preserve MSBs and OR with shifted bits
            var bits = (correctedValue & 0x4000) | (correctedValue >> 1);

            // write the shifted value into memory, but sign extended to fill 16 bits
            UnmodifiedWrite(SignExtend((ushort)bits));
        }
    }
}
