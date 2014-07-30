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

        public void Write(ushort value)
        {
            memory.WriteAndOverflowCorrect(value);
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
            return memory.ReadAndSignExtend();
        }
    }
}
