using Apollo.Virtual.AGC.Math;
using Apollo.Virtual.AGC.Memory;

namespace Apollo.Virtual.AGC.Registers
{
    class CycleRightRegister : MemoryWord
    {
        public CycleRightRegister(ushort address, MemoryBank bank)
            : base(address, bank)
        {
        }

        public override void Write(ushort value)
        {
            // get bit position 1, and move it to position 15
            var leastSignificateBit = (value & 0x1) << 14;

            // 15 bit register, so shift right 1, get the lower 14 bits, and add the wrap around bit
            var bits = (value >> 1 & 0x3FFF) | leastSignificateBit;

            // write the sign-extended cycled value into memory
            UnmodifiedWrite(SignExtend((ushort)bits));
        }
    }
}