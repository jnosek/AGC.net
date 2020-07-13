using System;
using System.Collections;
using System.Collections.Generic;

namespace Apollo.Virtual.AGC.Memory
{
    /// <summary>
    /// Memory bank that is implemented using a 16-bit (ushort) array
    /// can be configured with a static offset
    /// </summary>
    public class MemoryBank : IEnumerable<ushort>
    {
        private readonly ushort[] bank;
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
            get => bank[address - offset];
            set => bank[address - offset] = value;
        }

        internal void Copy(ushort[] data)
        {
            Array.Copy(data, bank, data.Length);
        }

        public IEnumerator<ushort> GetEnumerator() => new Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => bank.GetEnumerator();

        struct Enumerator : IEnumerator<ushort>
        {
            private int index;
            private readonly ushort[] array;

            internal Enumerator(MemoryBank memoryBank)
            {
                index = -1;
                Current = 0;
                array = memoryBank.bank;
            }

            public ushort Current { get; private set; }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if(index < array.Length)
                {
                    Current = array[index];
                    index++;
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                index = 0;
                Current = 0;
            }
        }
    }
}
