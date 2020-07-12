using Apollo.Virtual.AGC.Instructions;
using Apollo.Virtual.AGC.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class AddToStorageTests : BaseTest
    {
        private static readonly ushort instruction = AddToStorage.Encode(0x201);

        [TestMethod]
        public void AddToStorage_TwoPositiveNumbers()
        {
            // arrange
            Memory[0x000] = (10).ToOnesCompliment();
            Memory[0x201] = (15).ToOnesCompliment();

            // act - run the instructions
            RunInstruction(instruction);

            // assert
            CustomAssert.AreEqual(25, Memory[0x0]);
            CustomAssert.AreEqual(25, Memory[0x201]);
        }

        [TestMethod]
        public void AddToStorage_TwoNegativeNumbers()
        {
            // arrange
            Memory[0x000] = (~10).ToOnesCompliment(); // -10
            Memory[0x201] = (~15).ToOnesCompliment(); // -15

            // act - run the instructions
            RunInstruction(instruction);

            // assert
            CustomAssert.AreEqual(~25, Memory[0x0]);   // -25
            CustomAssert.AreEqual(~25, Memory[0x201]); // -25
        }
    }
}
