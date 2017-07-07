using Apollo.Virtual.AGC.Memory;

namespace Apollo.Virtual.AGC.Registers
{
    class FullRegister : MemoryWord, IWord
    {
        public FullRegister(ushort address, MemoryBank bank)
            : base(address, bank)
        {
        }

        public ushort Read()
        {
            return Get();
        }

        public void Write(ushort value)
        {
            Set(value);
        }
    }
}
