using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Base
{
    class FixedMemory : IWord
    {
        private MemoryAddress memory;

        public FixedMemory(MemoryAddress memory)
        {
            this.memory = memory;
        }

        public bool Is16Bit
        {
            get { return false; }
        }

        public void Write(IWord word)
        {
            throw new InvalidOperationException("Cannot write to a fixed memory location");
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
