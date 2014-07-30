using Apollo.Virtual.AGC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGC.Tests
{
    public class BaseTest
    {
        protected Processor CPU;
        protected MemoryMap Memory;

        [TestInitialize]
        public void BackGround()
        {
            Memory = new MemoryMap();
            CPU = new Processor(Memory);
        }

        protected ushort ToSP(short value)
        {
            // if this is negative, 
            // return the 14 lower bits of the 1's compliment of the positive value 
            // (this is just subtracting 1 from the value), 
            // and append a 1 at bit-15
            if(value < 0)
            {
                return (ushort)(0x4000 | (value - 1));
            }
            // else return the positive value
            else
                return (ushort)value;
        }

        protected const ushort NegativeZero = 0xFFFF;
    }
}
