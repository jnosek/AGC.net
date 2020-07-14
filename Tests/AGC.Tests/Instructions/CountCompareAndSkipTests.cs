using Apollo.Virtual.AGC.Instructions;
using Apollo.Virtual.AGC.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class CountCompareAndSkipTests : BaseTest
    {
        [TestMethod]
        public void CountCompareSkip_PositiveNumber()
        {
            // arrange
            Memory[0x200] = 10;

            // act
            RunInstruction(CountCompareAndSkip.Encode(0x200));

            // assert

            // check that number was decremented
            Assert.AreEqual(9, Memory[0x0]);

            // check that program counter advanced 1
            Assert.AreEqual(0x801, Memory[0x05]);
        }

        [TestMethod]
        public void CountCompareSkip_PositiveZero()
        {
            // arrange
            Memory[0x200] = OnesCompliment.PositiveZero;

            // act
            RunInstruction(CountCompareAndSkip.Encode(0x200));

            // assert

            // check that number was not decremented
            Assert.AreEqual(0, Memory[0x0]);

            // check that program counter advanced 2
            Assert.AreEqual(0x802, Memory[0x05]);
        }

        [TestMethod]
        public void CountCompareSkip_NegativeZero()
        {
            // arrange
            Memory[0x200] = OnesCompliment.NegativeZero;

            // act
            RunInstruction(CountCompareAndSkip.Encode(0x200));

            // assert

            // check that number was set to positive 0
            Assert.AreEqual(0, Memory[0x0]);

            // check that program counter advanced 4
            Assert.AreEqual(0x804, Memory[0x05]);
        }

        [TestMethod]
        public void CountCompareSkip_Negative()
        {
            // arrange
            Memory[0x200] = OnesCompliment.Convert(-10);

            // act
            RunInstruction(CountCompareAndSkip.Encode(0x200));

            // assert

            // check that number ABS - 1
            Assert.AreEqual(9, Memory[0x0]);

            // check that program counter advanced 3
            Assert.AreEqual(0x803, Memory[0x05]);
        }

        // TODO: added +0 and -0 tests

        [TestMethod]
        public void CountCompareSkip_PositiveOverflowAnd1()
        {
            // arrange

            // set A to 1 with positive overflow
            Memory[0x000] = 0x4000 | 1;

            // act
            RunInstruction(CountCompareAndSkip.Encode(0x000));

            // assert

            // check that A contains +0 and +overflow
            Assert.AreEqual(OnesCompliment.PositiveZero | 0x4000, Memory[0x0]);

            // check that program counter advanced 1
            Assert.AreEqual(0x801, Memory[0x05]);
        }

        [TestMethod]
        public void CountCompareSkip_PositiveOverflowAnd0()
        {
            // arrange

            // set A to 0 with positive overflow
            Memory[0x000] = 0x4000 | 0;

            // act
            RunInstruction(CountCompareAndSkip.Encode(0x000));

            // assert

            // check that A contains value without overflow
            Assert.AreEqual(0x3FFF, Memory[0x0]);

            // check that program counter advanced 1
            Assert.AreEqual(0x801, Memory[0x05]);
        }

        [TestMethod]
        public void CountCompareSkip_NegativeOverflow()
        {
            // arrange

            // set A to -2 with negative overflow
            Memory[0x000] = (ushort)(OnesCompliment.Convert(-2) & 0xBFFF);

            // act
            RunInstruction(CountCompareAndSkip.Encode(0x000));

            // assert

            // ensure result has positove overflow and is decremented
            Assert.AreEqual((0x4000 | 0x0001), Memory[0x0]);

            // check that program counter advanced 3
            Assert.AreEqual(0x803, Memory[0x05]);
        }
    }
}
