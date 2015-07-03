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
    class ProgramCounter: MemoryAddress, IWord
    {
        public ProgramCounter(MemoryBank bank)
            : base(0x05, bank)
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
