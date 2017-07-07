namespace Apollo.Virtual.AGC.Memory
{
    public abstract class MemoryWord
    {
        MemoryBank bank;

        public ushort Address { get; private set; }

        public MemoryWord(ushort address, MemoryBank bank)
        {
            this.bank = bank;
            this.Address = address;
        }

        protected ushort Get()
        {
            return bank[Address];
        }

        protected void Set(ushort value)
        {
            bank[Address] = value;
        }

        protected void Set(uint value)
        {
            bank[Address] = (ushort)value;
        }

        protected void Set(int value)
        {
            bank[Address] = (ushort)value;
        }
    }
}
