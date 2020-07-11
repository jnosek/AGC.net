using Apollo.Virtual.AGC.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class BranchZeroToFixed : BaseTest
    {
        [TestMethod]
        public void BranchZeroToFixed_PositiveZero()
        {
            // arrange
            Memory[0x0] = OnesCompliment.PositiveZero;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x01000 | 0x400 // instruction and address in fixed memory
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert

            // check for address to jump to in Z register
            CustomAssert.AreEqual(0x400, Memory[0x005]);
        }

        [TestMethod]
        public void BranchZeroToFixed_NegativeZero()
        {
            // arrange
            Memory[0x0] = OnesCompliment.NegativeZero;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x01000 | 0x500 // instruction and address in fixed memory
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert

            // check for address to jump to in Z register
            CustomAssert.AreEqual(0x500, Memory[0x005]);
        }

        [TestMethod]
        public void BranchZeroToFixed_NoJump()
        {
            // arrange
            Memory[0x0] = (5).ToOnesCompliment();

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x01000 | 0x500 // instruction and address in fixed memory
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert

            // check for address to jump to in Z register
            CustomAssert.AreEqual(0x802, Memory[0x005]);
        }
    }
}
