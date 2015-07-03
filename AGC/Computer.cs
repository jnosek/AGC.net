using Apollo.Virtual.AGC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC
{
    public class Computer
    {
        public MemoryMap Memory;
        
        public Processor CPU;

        public Computer()
        {
            Memory = new MemoryMap();
            CPU = new Processor(Memory);
        }

        public void Start()
        {
            CPU.Execute();
        }
    }
}
