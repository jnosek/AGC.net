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

        public void Write(ushort value)
        {
            //throw new InvalidOperationException("Cannot write to a fixed memory location");
        }

        public ushort Address
        {
            get { return memory.Address; }
        }

        public ushort Read()
        {
            return memory.ReadAndSignExtend();
        }
    }
}
