using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Base
{
    class ErasableMemory : IWord
    {
        protected MemoryAddress memory;

        public ErasableMemory()
        {
        }

        public ErasableMemory(MemoryAddress memory)
        {
            this.memory = memory;
        }

        public void Write(IWord word)
        {
            // if 16-bit, apply overflow correction and write 15-bit value
            if (word.Is16Bit)
            {
                uint value = word.Read();

                // get lower 14 bits
                uint lowerBits = value & 0x3FFF;

                // move 16-th bit, into 15th position, isolate it, and set it in above value;
                value = (value >> 1 & 0x4000) | lowerBits;

                memory.Write((ushort)value);
            }
            else
                memory.Write(word.Read());
        }

        public bool Is16Bit
        {
            get { return false; }
        }


        public ushort Address
        {
            get { return memory.Address; }
        }

        public ushort Read()
        {
            return memory.Read();
        }
    }
}
