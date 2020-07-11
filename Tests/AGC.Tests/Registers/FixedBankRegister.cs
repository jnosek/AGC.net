using Apollo.Virtual.AGC.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Registers
{
    [TestClass]
    public class FixedBankRegister : BaseTest
    {
        [TestMethod]
        public void FixedBankRegister_SetWithinMask()
        {
            // arrange
            Memory[0x200] = (0x0800).ToOnesCompliment();

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200, // add to the accumulator
                0x05800 | 0x04  // transfer to storage, the FB register
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            CustomAssert.AreEqual(0x0800, Memory[0x4]);

            // check BB register
            CustomAssert.AreEqual(0x0800, Memory[0x6]);
        }

        [TestMethod]
        public void FixedBankRegister_SetOutsideMask()
        {
            // arrange

            Memory[0x200] = (0x0002).ToOnesCompliment();

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200, // add to the accumulator
                0x05800 | 0x04  // transfer to storage, the FB register
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            CustomAssert.AreEqual(0x0000, Memory[0x4]);

            // check BB register
            CustomAssert.AreEqual(0x0000, Memory[0x6]);
        }
    }
}
