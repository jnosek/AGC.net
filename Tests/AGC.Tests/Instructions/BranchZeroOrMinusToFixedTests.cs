using Apollo.Virtual.AGC.Instructions;
using Apollo.Virtual.AGC.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class BranchZeroOrMinusToFixedTests : BaseTest
    {
        private static readonly ushort[] baseProgram = new[] {
            Extend.Instruction,
            BranchZeroOrMinusToFixed.Encode(0x500)
        };

        [TestMethod]
        public void BranchZeroOrMinusToFixed_PositiveZero()
        {
            // arrange
            Memory[0x0] = OnesCompliment.PositiveZero;

            // act - run the instructions
            RunProgram(baseProgram);

            // assert

            // check for address to jump to in Z register
            Assert.AreEqual(0x500, Memory[0x005]);
        }

        [TestMethod]
        public void BranchZeroOrMinusToFixed_NegativeZero()
        {
            // arrange
            Memory[0x0] = OnesCompliment.NegativeZero;

            // act - run the instructions
            RunProgram(baseProgram);

            // assert

            // check for address to jump to in Z register
            Assert.AreEqual(0x500, Memory[0x005]);
        }

        [TestMethod]
        public void BranchZeroOrMinusToFixed_Negative()
        {
            // arrange
            Memory[0x0] = OnesCompliment.Convert(-5);

            // act - run the instructions
            RunProgram(baseProgram);

            // assert

            // check for address to jump to in Z register
            Assert.AreEqual(0x500, Memory[0x005]);
        }

        [TestMethod]
        public void BranchZeroOrMinusToFixed_NoJump()
        {
            // arrange
            Memory[0x0] = 5;

            // act - run the instructions
            RunProgram(baseProgram);

            // assert

            // check for address to jump to in Z register
            Assert.AreEqual(0x802, Memory[0x005]);
        }
    }
}
