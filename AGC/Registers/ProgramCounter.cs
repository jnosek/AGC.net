using Apollo.Virtual.AGC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Registers
{
    /// <summary>
    /// 12-bit register for the address of the next instruction
    /// </summary>
    class ProgramCounter: MemoryWord, IWord
    {
        public ProgramCounter(ushort address, MemoryBank bank)
            : base(address, bank)
        {
        }

        public void Increment()
        {
            var value = Get();

            // increment
            value++;

            Write(value);
        }

        public void Write(ushort value)
        {
            // only store lower 12 bits
            Set(value & 0xFFF);
        }

        public ushort Read()
        {
            return Get();
        }
    }
}
