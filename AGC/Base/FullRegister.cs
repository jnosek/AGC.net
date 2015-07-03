﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Base
{
    class FullRegister : MemoryAddress, IWord
    {
        public FullRegister(ushort address, MemoryBank bank)
            : base(address, bank)
        {
        }

        public ushort Read()
        {
            return Get();
        }

        public void Write(ushort value)
        {
            Set(value);
        }
    }
}
