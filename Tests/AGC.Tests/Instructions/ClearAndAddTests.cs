using Apollo.Virtual.AGC.Instructions;
using Apollo.Virtual.AGC.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class ClearAndAddTests : BaseTest
    {
        [TestMethod]
        public void ClearAndAdd_FixedMemory()
        {
            // arrange

            // insert instructions and test data
            Memory.LoadFixedRom(new ushort [] {
                ClearAndAdd.Encode(0x801), // CA instruction and address
                10 //value
            });

            // act - run only one instruction
            CPU.Execute();

            // assert

            // check for value in the accumulator
            Assert.AreEqual(10, Memory[0x0]);
        }

        [TestMethod]
        public void ClearAndAdd_EraseableMemory()
        {
            // arrange
            Memory[0x200] = 10;

            // act - run the instructions
            RunInstruction(ClearAndAdd.Encode(0x200));

            // assert

            // check for value in the accumulator
            Assert.AreEqual(10, Memory[0x0]);
        }
    }
}
