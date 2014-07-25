using Apollo.Virtual.AGC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC
{
    class Computer
    {
        // 2048 words of 16 bit memory
        public Memory RAM;
        
        public Processor CPU;

        public Computer()
        {
            RAM = new Memory(2048);
            CPU = new Processor(RAM);
        }
    }
}
