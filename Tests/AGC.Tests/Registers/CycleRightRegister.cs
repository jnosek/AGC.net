using Apollo.Virtual.AGC.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Registers
{
    [TestClass]
    public class CycleRightRegister : BaseTest
    {
        [TestMethod]
        public void CycleRight_OneWrapAround()
        {
            // arrange
            Memory[0x200] = 51;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200, // add to the accumulator
                0x05800 | 0x010  // transfer to storage, the CYR register
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            Assert.AreEqual(0xC019, Memory[0x10]);
        }

        [TestMethod]
        public void CycleRight_ZeroWrapAround()
        {
            // arrange
            Memory[0x200] = 50;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200, // add to the accumulator
                0x05800 | 0x010  // transfer to storage, the CYR register
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            Assert.AreEqual(0X19, Memory[0x10]);
        }
    }
}
