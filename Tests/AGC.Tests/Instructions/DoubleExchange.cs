using Apollo.Virtual.AGC;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class DoubleExchange : BaseTest
    {
        [TestMethod]
        public void DoubleExchange_DefaultSwap()
        {
            // arrange

            // ensure overflow correction occurs
            Memory[0x000] = (0x8000).ToOnesCompliment();
            Memory[0x001] = (2).ToOnesCompliment();

            Memory[0x200] = (3).ToOnesCompliment();
            Memory[0x201] = (4).ToOnesCompliment();

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x05400 | 0x201
            });

            // act - run the instruction
            CPU.Execute();

            // assert
            CustomAssert.AreEqual(3, Memory[0x0]);
            CustomAssert.AreEqual(4, Memory[0x1]);

            CustomAssert.AreEqual(0xC000, Memory[0x200]);
            CustomAssert.AreEqual(2, Memory[0x201]);
        }

        [TestMethod]
        public void DoubleExchange_SwitchingBothBanks()
        {
            // arrange
            Memory[0x000] = (0x000D).ToOnesCompliment();
            Memory[0x001] = (0x0800 | 0x0002).ToOnesCompliment();

            Memory[0x006] = OnesCompliment.PositiveZero;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x05400 | 0x006
            });

            // act - run the instruction
            CPU.Execute();

            // assert

            // should be set to boot interrupt + 1 instruction
            CustomAssert.AreEqual(0x0801, Memory[0x0]); 
            CustomAssert.AreEqual(0x0000, Memory[0x1]);

            CustomAssert.AreEqual(0x000D, Memory[0x005]);
            CustomAssert.AreEqual(0x0802, Memory[0x006]);

            // check eb and fb registers
            CustomAssert.AreEqual(0x0200, Memory[0x03]);
            CustomAssert.AreEqual(0x0800, Memory[0x04]);
        }

        [TestMethod]
        public void DoubleExchange_SwitchingFBank()
        {
            // arrange
            Memory[0x000] = (0x0800).ToOnesCompliment(); 
            Memory[0x001] = (0x000D).ToOnesCompliment();

            Memory[0x004] = OnesCompliment.PositiveZero;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x05400 | 0x005
            });

            // act - run the instruction
            CPU.Execute();

            // assert

            CustomAssert.AreEqual(0x0000, Memory[0x0]);

            // should be set to boot interrupt + 1 instruction
            CustomAssert.AreEqual(0x0801, Memory[0x1]);

            CustomAssert.AreEqual(0x0800, Memory[0x004]);
            CustomAssert.AreEqual(0x000D, Memory[0x005]);

            // check fb and bb registers
            CustomAssert.AreEqual(0x0800, Memory[0x04]);
            CustomAssert.AreEqual(0x0800, Memory[0x06]);
        }

        /// <summary>
        /// This tests that full 16-bit value of A is exchanged with Q
        /// </summary>
        [TestMethod]
        public void DoubleExchange_QRegister()
        {
            // arrange
            Memory[0x000] = (0x8000).ToOnesCompliment();
            Memory[0x001] = (0x000D).ToOnesCompliment();

            Memory[0x002] = (0x8001).ToOnesCompliment();
            Memory[0x003] = (0x0400).ToOnesCompliment();

            Memory.LoadFixedRom(new ushort[]
            {
                0x05400 | 0x003
            });

            // act
            CPU.Execute();

            // assert

            CustomAssert.AreEqual(0x8001, Memory[0x0]);
            CustomAssert.AreEqual(0x0400, Memory[0x1]);

            CustomAssert.AreEqual(0x8000, Memory[0x2]);
            CustomAssert.AreEqual(0x0000, Memory[0x3]);
        }

        [TestMethod]
        public void DoubleExchange_LRegister()
        {
            // arrange
            Memory[0x000] = (0x8000).ToOnesCompliment();
            Memory[0x001] = (0x000D).ToOnesCompliment();
            Memory[0x002] = (0x8001).ToOnesCompliment();

            Memory.LoadFixedRom(new ushort[]
            {
                0x05400 | 0x002
            });

            // act
            CPU.Execute();

            // assert

            CustomAssert.AreEqual(0x8001, Memory[0x0]);
            // this value becomes overflow corrected because L is a 15-bit register
            CustomAssert.AreEqual(0xC000, Memory[0x1]);
            CustomAssert.AreEqual(0x000D, Memory[0x2]);
        }
    }
}
