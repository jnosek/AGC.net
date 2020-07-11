using Apollo.Virtual.AGC.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class ClearAndAdd : BaseTest
    {
        [TestMethod]
        public void ClearAndAdd_FixedMemory()
        {
            // arrange
            
            // insert instructions and test data
            Memory.LoadFixedRom(new ushort[] {
                0x03000 | 0x801, // CA instruction and address
                10 //value
            });

            // act - run the instructions
            CPU.Execute();

            // assert

            // check for value in the accumulator
            CustomAssert.AreEqual(10, Memory[0x0]);
        }

        [TestMethod]
        public void ClearAndAdd_EraseableMemory()
        {
            // arrange
            Memory[0x200] = (10).ToOnesCompliment();

            // insert instructions and test data
            Memory.LoadFixedRom(new ushort[] {
                0x03000 | 0x200 // CA instruction and address
            });

            // act - run the instructions
            CPU.Execute();

            // assert

            // check for value in the accumulator
            CustomAssert.AreEqual(10, Memory[0x0]);
        }
    }
}
