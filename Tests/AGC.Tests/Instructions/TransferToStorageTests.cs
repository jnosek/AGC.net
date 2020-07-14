using Apollo.Virtual.AGC.Instructions;
using Apollo.Virtual.AGC.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class TransferToStorageTests : BaseTest
    {
        [TestMethod]
        public void TransferToStorage_Default()
        {
            // arrange
            Memory[0x0] = 0x3;

            // act
            RunInstruction(TransferToStorage.Encode(0x0200));

            // assert
            Assert.AreEqual(0x3, Memory[0x200]);
            Assert.AreEqual(0x801, Memory[0x5]); // check Z register
        }

        [TestMethod]
        public void TransferToStorage_PositiveOverflow()
        {
            // arrange
            Memory[0x0] = 0x4001;

            // act
            RunInstruction(TransferToStorage.Encode(0x0200));

            // assert
            Assert.AreEqual(0x0001, Memory[0x200]);
            Assert.AreEqual(0x802, Memory[0x5]); // check Z register
            Assert.AreEqual(OnesCompliment.PositiveOne, Memory[0x0]); // check A register
        }

        [TestMethod]
        public void TransferToStorage_NegativeOverflow()
        {
            // arrange
            Memory[0x0] = 0xBFFD;

            // act
            RunInstruction(TransferToStorage.Encode(0x0200));

            // assert
            Assert.AreEqual(0xFFFD, Memory[0x200]);
            Assert.AreEqual(0x802, Memory[0x5]); // check Z register
            Assert.AreEqual(OnesCompliment.NegativeOne, Memory[0x0]); // check A register
        }
    }
}
