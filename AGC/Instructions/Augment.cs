using Apollo.Virtual.AGC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Instructions
{
    class Augment: IInstruction
    {
        public ushort Code
        {
            get { throw new NotImplementedException(); }
        }

        public Processor CPU { get; set; }

        public void Execute(ushort K)
        {
            throw new NotImplementedException();
        }
    }
}
