using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Base
{
    /// <summary>
    /// 15 bit memory location that is readwrite
    /// </summary>
    class ErasableMemory : MemoryAddress, IWord
    {
        public ErasableMemory(ushort address, MemoryBank bank)
            : base(address, bank)
        {
        }

        public void Write(ushort value)
        {
            var v = new OnesCompliment(value);
            v.OverflowCorrect();

            Set(v);
        }

        public ushort Read()
        {
            var v = new OnesCompliment(Get());
            v.SignExtend();

            return v;
        }
    }
}
