using Apollo.Virtual.AGC.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Registers
{
    [TestClass]
    public class ShiftRightRegister : BaseTest
    {
        [TestMethod]
        public void ShiftRight_Default()
        {
            // arrange
            Memory[0x200] = (0x202).ToOnesCompliment();

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200, // add to the accumulator
                0x05800 | 0x011  // transfer to storage, the SR register
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            CustomAssert.AreEqual(0x0101, Memory[0x11]);
        }

        [TestMethod]
        public void ShiftRight_ClearLSB()
        {
            // arrange
            Memory[0x200] = (0x201).ToOnesCompliment();

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200, // add to the accumulator
                0x05800 | 0x011  // transfer to storage, the SR register
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            CustomAssert.AreEqual(0x0100, Memory[0x11]);
        }

        [TestMethod]
        public void ShiftRight_DuplicateMSB()
        {
            // arrange
            Memory[0x200] = (0xC000).ToOnesCompliment(); // sign extended 0x4000 value

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200, // add to the accumulator
                0x05800 | 0x011  // transfer to storage, the SR register
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            // sign extended 0x6000 value
            CustomAssert.AreEqual(0xE000, Memory[0x11]);
        }
    }
}
