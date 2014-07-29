using Apollo.Virtual.AGC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Registers
{
    class Register : ErasableMemory, IRegister
    {
        MemoryAddress IRegister.Memory
        {
            set { this.memory = memory; }
        }
    }
}
