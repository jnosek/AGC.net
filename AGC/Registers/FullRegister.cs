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

        public bool Is16Bit
        {
            get { return true; }
        }

        public void Write(IWord word)
        {
            if (word.Is16Bit)
                memory.Write(word.Read());
            // if a 15-bit word, must sign extend it
            else
            {
                uint value = word.Read();

                // take lower 15-bits
                value = value & 0x7FFF;

                // shift left 1 and take 16th bit, combine with lower 15 bits
                value = ((value << 1) & 0x8000) | value;

                memory.Write((ushort)value);
            }
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
