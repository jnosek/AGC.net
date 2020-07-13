using Apollo.Virtual.AGC.Math;
using Apollo.Virtual.AGC.Memory;

namespace Apollo.Virtual.AGC.Registers
{
    class AccumulatorRegister : FullRegister
    {
        public AccumulatorRegister(ushort address, MemoryBank bank)
            : base(address, bank)
        {
        }

        public void Add(ushort value)
        {
            var sum = OnesCompliment.Add(value, Read());

            Write(sum);
        }

        public bool IsOverflow
        {
            get
            {
                // look at bits 16 and 15 to see if they are different
                var value = Read() & 0xC000;

                return value == 0x8000 || value == 0x4000;
            }
        }
    }
}
