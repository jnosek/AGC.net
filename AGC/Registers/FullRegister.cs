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

        public bool IsOverflow
        {
            get
            {
                // look at bits 16 and 15 to see if they are different
                var value = (ushort)(Read() & 0xC000);

                return value == 0x8000 || value == 0x4000;
            }
        }

        MemoryAddress IRegister.Memory
        {
            set { this.memory = value; }
        }
    }
}
