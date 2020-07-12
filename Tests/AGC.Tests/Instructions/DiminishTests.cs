using Apollo.Virtual.AGC.Instructions;
using Apollo.Virtual.AGC.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class DiminishTests : BaseTest
    {
        private static readonly ushort[] baseProgram = new []
        {
            Extend.Instruction,
            Diminish.Encode(0x200)
        };

        [TestMethod]
        public void Diminish_PositiveNumber()
        {
            // arrange
            Memory[0x200] = (10).ToOnesCompliment();

            // act - run the instructions
            RunProgram(baseProgram);

            // assert
            CustomAssert.AreEqual(9, Memory[0x200]);
        }

        [TestMethod]
        public void Diminish_NegativeNumber()
        {
            // arrange
            Memory[0x200] = (~10).ToOnesCompliment();  // -10

            // act - run the instructions
            RunProgram(baseProgram);

            // assert
            CustomAssert.AreEqual(~9, Memory[0x200]); // -9
        }

        [TestMethod]
        public void Diminish_16BitRegister()
        {
            // arrange
            Memory[0x00] = (0x4001).ToOnesCompliment(); // 15-bit positive value

            // act - run the instructions
            RunProgram(new ushort[] {
                Extend.Instruction,
                Diminish.Encode(0x000)
            }); ;

            // assert
            CustomAssert.AreEqual(0x4000, Memory[0x000]);
        }
    }
}
