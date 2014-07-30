using Apollo.Virtual.AGC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Registers
{
    /// <summary>
    /// 12-bit register for the address of the next instruction
    /// </summary>
    class ProgramCounter: IWord, IRegister
    {
        MemoryAddress memory;

        public void Increment()
        {
            var value = memory.Read();

            // increment
            value++;

            Write(value);
        }

        public void Write(ushort value)
        {
            // only store lower 12 bits
            memory.Write((ushort)(value & 0xFFF));
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
