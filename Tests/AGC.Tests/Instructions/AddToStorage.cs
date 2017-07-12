using Apollo.Virtual.AGC;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class AddToStorage : BaseTest
    {
        [TestMethod]
        public void AddToStorage_TwoPositiveNumbers()
        {
            // arrange
            Memory[0x000] = (10).ToOnesCompliment();
            Memory[0x201] = (15).ToOnesCompliment();

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x02C00 | 0x201
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            CustomAssert.AreEqual(25, Memory[0x0]);
            CustomAssert.AreEqual(25, Memory[0x201]);
        }

        [TestMethod]
        public void AddToStorage_TwoNegativeNumbers()
        {
            // arrange
            Memory[0x000] = (~10).ToOnesCompliment(); // -10
            Memory[0x201] = (~15).ToOnesCompliment(); // -15

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x02C00 | 0x201
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            CustomAssert.AreEqual(~25, Memory[0x0]);   // -25
            CustomAssert.AreEqual(~25, Memory[0x201]); // -25
        }
    }
}
