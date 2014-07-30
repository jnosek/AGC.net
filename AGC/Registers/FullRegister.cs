using Apollo.Virtual.AGC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Registers
{
    /// <summary>
    /// Register that holds a 16-bit value
    /// </summary>
    class FullRegister: IWord, IRegister
    {
        protected MemoryAddress memory;

        public void Write(ushort value)
        {
            memory.Write(value);
        }

        public ushort Address
        {
            get { return memory.Address; }
        }

        public ushort Read()
        {
            return memory.Read();
        }

        MemoryAddress IRegister.Memory
        {
            set { this.memory = value; }
        }
    }
}
