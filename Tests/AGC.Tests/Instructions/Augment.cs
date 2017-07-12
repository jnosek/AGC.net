using Apollo.Virtual.AGC;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class Augment : BaseTest
    {
        [TestMethod]
        public void Augment_PositiveNumber()
        {
            // arrange
            Memory[0x200] = (10).ToOnesCompliment();

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x02800 | 0x200
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            CustomAssert.AreEqual(11, Memory[0x200]);
        }

        [TestMethod]
        public void Augment_NegativeNumber()
        {
            // arrange
            Memory[0x200] = (~10).ToOnesCompliment();  // -10

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x02800 | 0x200
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            CustomAssert.AreEqual(~11, Memory[0x200]); // -11
        }

        [TestMethod]
        public void Augment_16BitRegister()
        {
            // arrange
            Memory[0x00] = (0x4001).ToOnesCompliment(); // 15-bit positive value

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x02800 | 0x000
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            CustomAssert.AreEqual(0x4002, Memory[0x000]);
        }
    }
}
