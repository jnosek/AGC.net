using Apollo.Virtual.AGC.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class DoubleClearAndAdd : BaseTest
    {
        [TestMethod]
        public void DoubleClearAndAdd_Default()
        {
            // arrange
            Memory[0x200] = (0x020).ToOnesCompliment();
            Memory[0x201] = (0x040).ToOnesCompliment();

            // insert instructions and test data
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x03000 | 0x201,
            });

            // act
            CPU.Execute();
            CPU.Execute();

            // assert

            // check value in accumulator and L
            CustomAssert.AreEqual(0x020, Memory[0x0]);
            CustomAssert.AreEqual(0x040, Memory[0x1]);
        }

        [TestMethod]
        public void DoubleClearAndAdd_CycleRegister()
        {
            // arrange

            // writing int CYR will cause it to cycle right
            Memory[0x010] = (0x002).ToOnesCompliment();

            // writing into SR will cause it to shift right
            Memory[0x011] = (0x004).ToOnesCompliment();

            // insert instructions and test data
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x03000 | 0x011, // SR address
            });

            // act
            CPU.Execute();
            CPU.Execute();

            // assert

            // check value in accumulator and L
            CustomAssert.AreEqual(0x001, Memory[0x0]);
            CustomAssert.AreEqual(0x002, Memory[0x1]);

            // check value in CYR was cycled
            CustomAssert.AreEqual(0xC000, Memory[0x10]);

            // check value in SR was shifted
            CustomAssert.AreEqual(0x001, Memory[0x11]);
        }

        [TestMethod]
        public void DoubleClearAndAdd_LRegister()
        {
            // arrange
            Memory[0x002] = (0x020).ToOnesCompliment();

            // insert instructions and test data
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x03000 | 0x002, // Q address
            });

            // act
            CPU.Execute();
            CPU.Execute();

            // assert

            // check value in accumulator and L
            CustomAssert.AreEqual(0x020, Memory[0x0]);
            CustomAssert.AreEqual(0x020, Memory[0x1]);
        }

        [TestMethod]
        public void DoubleClearAndAdd_QRegister()
        {
            // arrange
            Memory[0x002] = (0xF000).ToOnesCompliment();
            Memory[0x003] = (0x0200).ToOnesCompliment();

            // insert instructions and test data
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x03000 | 0x003, // EB address
            });

            // act
            CPU.Execute();
            CPU.Execute();

            // assert

            // check value in accumulator and L
            CustomAssert.AreEqual(0xF000, Memory[0x0]);
            CustomAssert.AreEqual(0x0200, Memory[0x1]);
        }
    }
}
