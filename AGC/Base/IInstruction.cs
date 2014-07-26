using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Base
{
    interface IInstruction
    {
        ushort Code { get; }

        Processor CPU { get; set; }
        
        void Execute(ushort K);
    }
}
