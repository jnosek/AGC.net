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
            uint value = memory.Read();

            // increment
            value++;

            // only store lower 12 bits
            memory.Write((ushort)(value & 0xFFF));
        }

        public bool Is16Bit
        {
            get { return false; }
        }

        public void Write(IWord word)
        {
            // only store lower 12 bits
            memory.Write((ushort)(word.Read() & 0xFFF));
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
