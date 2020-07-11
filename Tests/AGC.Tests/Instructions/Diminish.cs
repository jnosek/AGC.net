using Apollo.Virtual.AGC.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class Diminish : BaseTest
    {
        [TestMethod]
        public void Diminish_PositiveNumber()
        {
            // arrange
            Memory[0x200] = (10).ToOnesCompliment();

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x02C00 | 0x200
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            CustomAssert.AreEqual(9, Memory[0x200]);
        }

        [TestMethod]
        public void Diminish_NegativeNumber()
        {
            // arrange
            Memory[0x200] = (~10).ToOnesCompliment();  // -10

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x02C00 | 0x200
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            CustomAssert.AreEqual(~9, Memory[0x200]); // -9
        }

        [TestMethod]
        public void Diminish_16BitRegister()
        {
            // arrange
            Memory[0x00] = (0x4001).ToOnesCompliment(); // 15-bit positive value

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x02C00 | 0x000
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            CustomAssert.AreEqual(0x4000, Memory[0x000]);
        }
    }
}
