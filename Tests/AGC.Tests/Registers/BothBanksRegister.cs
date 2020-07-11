using Apollo.Virtual.AGC.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Registers
{
    [TestClass]
    public class BothBanksRegister : BaseTest
    {
        [TestMethod]
        public void BothBanksRegister_SetWithinMask()
        {
            // arrange
            Memory[0x200] = (0x0800 | 0x0002).ToOnesCompliment();

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200, // add to the accumulator
                0x05800 | 0x06  // transfer to storage, the BB register
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert

            // check BB register
            CustomAssert.AreEqual(0x0800 | 0x0002, Memory[0x6]);

            // check EB register
            CustomAssert.AreEqual(0x0200, Memory[0x3]);

            // check FB register
            CustomAssert.AreEqual(0x0800, Memory[0x4]);
        }

        [TestMethod]
        public void BothBanksRegister_SetOutsideMask()
        {
            // arrange
            Memory[0x200] = (0x0020).ToOnesCompliment();

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200, // add to the accumulator
                0x05800 | 0x06  // transfer to storage, the BB register
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert

            // check BB register
            CustomAssert.AreEqual(0x0000, Memory[0x6]);

            // check EB register
            CustomAssert.AreEqual(0x0000, Memory[0x3]);

            // check FB register
            CustomAssert.AreEqual(0x0000, Memory[0x4]);
        }
    }
}
