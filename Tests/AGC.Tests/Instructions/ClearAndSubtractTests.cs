using Apollo.Virtual.AGC.Instructions;
using Apollo.Virtual.AGC.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class ClearAndSubtractTests : BaseTest
    {
        [TestMethod]
        public void ClearAndSubtract_Accumulator()
        {
            // arrange
            Memory[0x0] = (0x0101).ToOnesCompliment();

            // act
            RunInstruction(Compliment.Instruction);

            // assert
            CustomAssert.AreEqual(0xFEFE, Memory[0x0]);
        }

        [TestMethod]
        public void ClearAndSubtract_Memory()
        {
            // arrange
            Memory[0x200] = (0xF0F0).ToOnesCompliment();

            // act
            RunInstruction(ClearAndSubtract.Encode(0x200));

            // assert
            CustomAssert.AreEqual(0x0F0F, Memory[0x0]);
        }
    }
}
