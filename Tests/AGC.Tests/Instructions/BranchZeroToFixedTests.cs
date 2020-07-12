using Apollo.Virtual.AGC.Instructions;
using Apollo.Virtual.AGC.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class BranchZeroToFixedTests : BaseTest
    {
        private static readonly ushort[] baseProgram = new[] {
            Extend.Instruction,
            BranchZeroOrMinusToFixed.Encode(0x500)
        };

        [TestMethod]
        public void BranchZeroToFixed_PositiveZero()
        {
            // arrange
            Memory[0x0] = OnesCompliment.PositiveZero;

            // act - run the instructions
            RunProgram(baseProgram);

            // assert

            // check for address to jump to in Z register
            CustomAssert.AreEqual(0x500, Memory[0x005]);
        }

        [TestMethod]
        public void BranchZeroToFixed_NegativeZero()
        {
            // arrange
            Memory[0x0] = OnesCompliment.NegativeZero;

            // act - run the instructions
            RunProgram(baseProgram);

            // assert

            // check for address to jump to in Z register
            CustomAssert.AreEqual(0x500, Memory[0x005]);
        }

        [TestMethod]
        public void BranchZeroToFixed_NoJump()
        {
            // arrange
            Memory[0x0] = (5).ToOnesCompliment();

            // act - run the instructions
            RunProgram(baseProgram);

            // assert

            // check for address to jump to in Z register
            CustomAssert.AreEqual(0x802, Memory[0x005]);
        }
    }
}
