using Apollo.Virtual.AGC.Instructions;
using Apollo.Virtual.AGC.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class AugmentTests : BaseTest
    {
        private static readonly ushort[] baseProgram = new[] {

            Extend.Instruction,
            Augment.Encode(0x200)
        };

        [TestMethod]
        public void Augment_PositiveNumber()
        {
            // arrange
            Memory[0x200] = 10;

            // act - run the instructions
            RunProgram(baseProgram);

            // assert
            Assert.AreEqual(11, Memory[0x200]);
        }

        [TestMethod]
        public void Augment_NegativeNumber()
        {
            // arrange
            Memory[0x200] = OnesCompliment.Convert(-10);

            // act - run the instructions
            RunProgram(baseProgram);

            // assert
            Assert.AreEqual(OnesCompliment.Convert(-11), Memory[0x200]);
        }

        [TestMethod]
        public void Augment_16BitRegister()
        {
            // arrange
            Memory[0x00] = 0x4001; // 15-bit positive value

            // act - run the instructions
            RunProgram(new [] {
                Extend.Instruction, // EXTEND instruction
                Augment.Encode(0x000)
            });

            // assert
            Assert.AreEqual(0x4002, Memory[0x000]);
        }
    }
}
