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

        protected void RunProgram(ushort[] program)
        {
            // insert instructions
            Memory.LoadFixedRom(program);

            // act - run the instructions
            CPU.Execute(program.Length);
        }

        protected void RunInstruction(ushort instruction)
        {
            Memory.LoadFixedRom(new[] { instruction });

            // act - run the instructions
            CPU.Execute();
        }

        protected ushort Instruction(ushort code, ushort operand)
        {
            return (ushort)((code << 12) | operand);
        }
    }
}
