using Apollo.Virtual.AGC.Core;
using Apollo.Virtual.AGC.Memory;

namespace Apollo.Virtual.AGC.Registers
{
    class ErasableBankRegister : MemoryWord, IWord
    {
        public ErasableBankRegister(ushort address, MemoryBank bank)
            : base(address, bank)
        {
        }

        public ushort Read()
        {
            var v = new OnesCompliment(Get());
            v.SignExtend();
            return v;
        }

        public void Write(ushort value)
        {
            // can only set the 3 bits for the erasable memory bank selection
            Set(value & 0x0700);
        }
    }
}
