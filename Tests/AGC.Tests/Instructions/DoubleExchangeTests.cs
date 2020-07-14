using Apollo.Virtual.AGC.Instructions;
using Apollo.Virtual.AGC.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class DoubleExchangeTests : BaseTest
    {
        [TestMethod]
        public void DoubleExchange_DefaultSwap()
        {
            // arrange

            // ensure overflow correction occurs
            Memory[0x000] = 0x8000;
            Memory[0x001] = 2;

            Memory[0x200] = 3;
            Memory[0x201] = 4;

            // act - run the instruction
            RunInstruction(DoubleExchange.Encode(0x201));

            // assert
            Assert.AreEqual(3, Memory[0x0]);
            Assert.AreEqual(4, Memory[0x1]);

            Assert.AreEqual(0xC000, Memory[0x200]);
            Assert.AreEqual(2, Memory[0x201]);
        }

        [TestMethod]
        public void DoubleExchange_SwitchingBothBanks()
        {
            // arrange
            Memory[0x000] = 0x000D;
            Memory[0x001] = 0x0800 | 0x0002;

            Memory[0x006] = OnesCompliment.PositiveZero;

            // act - run the instruction
            RunInstruction(DoubleTransferControlSwitchingBothBanks.Instruction);

            // assert

            // should be set to boot interrupt + 1 instruction
            Assert.AreEqual(0x0801, Memory[0x0]); 
            Assert.AreEqual(0x0000, Memory[0x1]);

            Assert.AreEqual(0x000D, Memory[0x005]);
            Assert.AreEqual(0x0802, Memory[0x006]);

            // check eb and fb registers
            Assert.AreEqual(0x0200, Memory[0x03]);
            Assert.AreEqual(0x0800, Memory[0x04]);
        }

        [TestMethod]
        public void DoubleExchange_SwitchingFBank()
        {
            // arrange
            Memory[0x000] = 0x0800; 
            Memory[0x001] = 0x000D;

            Memory[0x004] = OnesCompliment.PositiveZero;

            // act - run the instruction
            RunInstruction(DoubleTransferControlSwitchingFBank.Instruction);

            // assert

            Assert.AreEqual(0x0000, Memory[0x0]);

            // should be set to boot interrupt + 1 instruction
            Assert.AreEqual(0x0801, Memory[0x1]);

            Assert.AreEqual(0x0800, Memory[0x004]);
            Assert.AreEqual(0x000D, Memory[0x005]);

            // check fb and bb registers
            Assert.AreEqual(0x0800, Memory[0x04]);
            Assert.AreEqual(0x0800, Memory[0x06]);
        }

        /// <summary>
        /// This tests that full 16-bit value of A is exchanged with Q
        /// </summary>
        [TestMethod]
        public void DoubleExchange_QRegister()
        {
            // arrange
            Memory[0x000] = 0x8000;
            Memory[0x001] = 0x000D;

            Memory[0x002] = 0x8001;
            Memory[0x003] = 0x0400;

            // act - run the instruction
            RunInstruction(DoubleExchange.Encode(0x003));

            // assert

            Assert.AreEqual(0x8001, Memory[0x0]);
            Assert.AreEqual(0x0400, Memory[0x1]);

            Assert.AreEqual(0x8000, Memory[0x2]);
            Assert.AreEqual(0x0000, Memory[0x3]);
        }

        [TestMethod]
        public void DoubleExchange_LRegister()
        {
            // arrange
            Memory[0x000] = 0x8000;
            Memory[0x001] = 0x000D;
            Memory[0x002] = 0x8001;

            // act - run the instruction
            RunInstruction(DoubleExchange.Encode(0x002));

            // assert

            Assert.AreEqual(0x8001, Memory[0x0]);
            // this value becomes overflow corrected because L is a 15-bit register
            Assert.AreEqual(0xC000, Memory[0x1]);
            Assert.AreEqual(0x000D, Memory[0x2]);
        }
    }
}
