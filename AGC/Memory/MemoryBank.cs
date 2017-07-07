using System;

namespace Apollo.Virtual.AGC.Memory
{
    /// <summary>
    /// Memory bank that is implemented using a 16-bit (ushort) array
    /// can be configured with a static offset
    /// </summary>
    public class MemoryBank
    {
        private ushort[] bank;
        private uint offset;

        public MemoryBank(uint size)
            : this(size, 0)
        {
        }

        public MemoryBank(uint size, uint offset)
        {
            bank = new ushort[size];
            this.offset = offset;
        }

        public virtual ushort this[uint address]
        {
            get
            {
                return bank[address - offset];
            }
            set
            {
                bank[address - offset] = value;
            }
        }

        internal void Copy(ushort[] data)
        {
            Array.Copy(data, bank, data.Length);
        }
    }
}
