using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Registers
{
    [TestClass]
    public class ErasableBankRegister : BaseTest
    {
        [TestMethod]
        public void ErasableBank_SetWithinMask()
        {
            // arrange
            Memory[0x200] = 0x0200;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200, // add to the accumulator
                0x05800 | 0x03  // transfer to storage, the EB register
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            Assert.AreEqual(0x0200, Memory[0x3]);
        }

        [TestMethod]
        public void ErasableBank_SetOutsideMask()
        {
            // arrange
            Memory[0x200] = 0x0002;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200, // add to the accumulator
                0x05800 | 0x03  // transfer to storage, the EB register
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            Assert.AreEqual(0x0, Memory[0x3]);
        }
    }
}
