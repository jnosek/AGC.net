using Apollo.Virtual.AGC;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class CountCompareAndSkip : BaseTest
    {
        [TestMethod]
        public void CountCompareSkip_PositiveNumber()
        {
            // arrange
            Memory[0x200] = (10).ToOnesCompliment();

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                Instruction(0x01, 0x200)
            });

            // act
            CPU.Execute();

            // assert

            // check that number was decremented
            CustomAssert.AreEqual(9, Memory[0x0]);

            // check that program counter advanced 1
            CustomAssert.AreEqual(0x801, Memory[0x05]);
        }

        [TestMethod]
        public void CountCompareSkip_PositiveZero()
        {
            // arrange
            Memory[0x200] = OnesCompliment.PositiveZero;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                Instruction(0x01, 0x200)
            });

            // act
            CPU.Execute();

            // assert

            // check that number was not decremented
            CustomAssert.AreEqual(0, Memory[0x0]);

            // check that program counter advanced 2
            CustomAssert.AreEqual(0x802, Memory[0x05]);
        }

        [TestMethod]
        public void CountCompareSkip_NegativeZero()
        {
            // arrange
            Memory[0x200] = OnesCompliment.NegativeZero;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                Instruction(0x01, 0x200)
            });

            // act
            CPU.Execute();

            // assert

            // check that number was set to positive 0
            CustomAssert.AreEqual(0, Memory[0x0]);

            // check that program counter advanced 4
            CustomAssert.AreEqual(0x804, Memory[0x05]);
        }

        [TestMethod]
        public void CountCompareSkip_Negative()
        {
            // arrange
            Memory[0x200] = (~10).ToOnesCompliment(); // -10

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                Instruction(0x01, 0x200)
            });

            // act
            CPU.Execute();

            // assert

            // check that number ABS - 1
            CustomAssert.AreEqual(9, Memory[0x0]);

            // check that program counter advanced 3
            CustomAssert.AreEqual(0x803, Memory[0x05]);
        }

        // TODO: added +0 and -0 tests

        [TestMethod]
        public void CountCompareSkip_PositiveOverflowAnd1()
        {
            // arrange

            // set A to 1 with positive overflow
            Memory[0x000] = new OnesCompliment(0x4000 | 1);

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                Instruction(0x01, 0x000) // CCS A
            });

            // act
            CPU.Execute();

            // assert

            // check that A contains +0 and +overflow
            CustomAssert.AreEqual(OnesCompliment.PositiveZero.NativeValue | 0x4000, Memory[0x0]);

            // check that program counter advanced 1
            CustomAssert.AreEqual(0x801, Memory[0x05]);
        }

        [TestMethod]
        public void CountCompareSkip_PositiveOverflowAnd0()
        {
            // arrange

            // set A to 0 with positive overflow
            Memory[0x000] = new OnesCompliment(0x4000 | 0);

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                Instruction(0x01, 0x000) // CCS A
            });

            // act
            CPU.Execute();

            // assert

            // check that A contains value without overflow
            CustomAssert.AreEqual(0x3FFF, Memory[0x0]);

            // check that program counter advanced 1
            CustomAssert.AreEqual(0x801, Memory[0x05]);
        }

        [TestMethod]
        public void CountCompareSkip_NegativeOverflow()
        {
            // arrange

            // set A to -2 with negative overflow
            Memory[0x000] = ((~2) & 0xBFFF).ToOnesCompliment();

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                Instruction(0x01, 0x000) // CCS A
            });

            // act
            CPU.Execute();

            // assert

            // ensure result has positove overflow and is decremented
            CustomAssert.AreEqual((0x4000 | 0x0001), Memory[0x0]);

            // check that program counter advanced 3
            CustomAssert.AreEqual(0x803, Memory[0x05]);
        }
    }
}
