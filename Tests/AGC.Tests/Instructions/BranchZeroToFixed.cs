using Apollo.Virtual.AGC.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class BranchZeroToFixed : BaseTest
    {
        [TestMethod]
        public void BranchZeroToFixedPositiveZero()
        {
            // arrange
            Memory[0x0] = 0;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x01000 | 0x400 // instruction and address in fixed memory
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert

            // check for address to jump to in Z register
            Assert.AreEqual(0x400, Memory[0x005]);
        }

        [TestMethod]
        public void BranchZeroToFixedNegativeZero()
        {
            // arrange
            Memory[0x0] = OnesCompliment.NegativeZero;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x01000 | 0x500 // instruction and address in fixed memory
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert

            // check for address to jump to in Z register
            Assert.AreEqual(0x500, Memory[0x005]);
        }

        [TestMethod]
        public void BranchZeroToFixedNoJump()
        {
            // arrange
            Memory[0x0] = 5;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x01000 | 0x500 // instruction and address in fixed memory
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert

            // check for address to jump to in Z register
            Assert.AreEqual(0x802, Memory[0x005]);
        }
    }
}
