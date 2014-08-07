using Apollo.Virtual.AGC.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class BranchZeroOrMinusToFixed : BaseTest
    {
        [TestMethod]
        public void BranchZeroOrMinusToFixedPositiveZero()
        {
            // arrange
            Memory[0x0] = 0;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x06000 | 0x400 // instruction and address in fixed memory
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert

            // check for address to jump to in Z register
            Assert.AreEqual(0x400, Memory[0x005]);
        }

        [TestMethod]
        public void BranchZeroOrMinusToFixedNegativeZero()
        {
            // arrange
            Memory[0x0] = SinglePrecision.NegativeZero;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x06000 | 0x500 // instruction and address in fixed memory
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert

            // check for address to jump to in Z register
            Assert.AreEqual(0x500, Memory[0x005]);
        }

        [TestMethod]
        public void BranchZeroOrMinusToFixedNegative()
        {
            // arrange
            Memory[0x0] = SinglePrecision.To(-5);

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x06000 | 0x500 // instruction and address in fixed memory
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert

            // check for address to jump to in Z register
            Assert.AreEqual(0x500, Memory[0x005]);
        }

        [TestMethod]
        public void BranchZeroOrMinusToFixedNoJump()
        {
            // arrange
            Memory[0x0] = 5;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x06000 | 0x500 // instruction and address in fixed memory
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
