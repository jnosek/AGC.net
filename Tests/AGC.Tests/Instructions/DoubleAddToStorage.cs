using Apollo.Virtual.AGC.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class DoubleAddToStorage : BaseTest
    {
        [TestMethod]
        public void DoubleAddToStorage_LswValue()
        {
            // arrange
            Memory[0x0] = OnesCompliment.PositiveZero;
            Memory[0x1] = OnesCompliment.PositiveOne;

            Memory[0x200] = OnesCompliment.PositiveZero;
            Memory[0x201] = OnesCompliment.PositiveOne;

            Memory.LoadFixedRom(new ushort[] { 
                Instruction(0x02, 0x201)
            });

            // act
            CPU.Execute();

            // assert
            CustomAssert.AreEqual(0, Memory[0x200]);
            CustomAssert.AreEqual(2, Memory[0x201]);
            CustomAssert.AreEqual(0, Memory[0x000]);
            Assert.AreEqual(OnesCompliment.PositiveZero, Memory[0x001]);
        }

        [TestMethod]
        public void DoubleAddToStorage_LswPositiveOverflow()
        {
            // arrange
            Memory[0x0] = OnesCompliment.PositiveZero;
            Memory[0x1] = (0x3FFF).ToOnesCompliment();

            Memory[0x200] = OnesCompliment.PositiveZero;
            Memory[0x201] = OnesCompliment.PositiveOne;

            Memory.LoadFixedRom(new ushort[] { 
                Instruction(0x02, 0x201)
            });

            // act
            CPU.Execute();

            // assert
            Assert.AreEqual(OnesCompliment.PositiveOne, Memory[0x200]);
            Assert.AreEqual(OnesCompliment.PositiveZero, Memory[0x201]);
            Assert.AreEqual(OnesCompliment.PositiveZero, Memory[0x000]);
            Assert.AreEqual(OnesCompliment.PositiveZero, Memory[0x001]);
        }

        [TestMethod]
        public void DoubleAddToStroage_LswNegativeUnderflow()
        {
            // arrange
            Memory[0x0] = OnesCompliment.NegativeZero;
            Memory[0x1] = (0xC000).ToOnesCompliment(); // largest negative number;

            Memory[0x200] = OnesCompliment.PositiveZero;
            Memory[0x201] = OnesCompliment.NegativeOne;

            Memory.LoadFixedRom(new ushort[] { 
                Instruction(0x02, 0x201)
            });

            // act
            CPU.Execute();

            // assert
            Assert.AreEqual(OnesCompliment.NegativeOne, Memory[0x200]);
            Assert.AreEqual(OnesCompliment.NegativeZero, Memory[0x201]);
            Assert.AreEqual(OnesCompliment.PositiveZero, Memory[0x000]);
            Assert.AreEqual(OnesCompliment.PositiveZero, Memory[0x001]);
        }

        [TestMethod]
        public void DoubleAddToStorage_MswPositiveOverflow()
        {
            // arrange
            Memory[0x0] = (0x3FFF).ToOnesCompliment();
            Memory[0x1] = (0x3FFF).ToOnesCompliment();

            Memory[0x200] = OnesCompliment.PositiveZero;
            Memory[0x201] = OnesCompliment.PositiveOne;

            Memory.LoadFixedRom(new ushort[] { 
                Instruction(0x02, 0x201)
            });

            // act
            CPU.Execute();

            // assert
            Assert.AreEqual(OnesCompliment.PositiveZero, Memory[0x200]);
            Assert.AreEqual(OnesCompliment.PositiveZero, Memory[0x201]);
            Assert.AreEqual(OnesCompliment.PositiveOne,  Memory[0x000]);
            Assert.AreEqual(OnesCompliment.PositiveZero, Memory[0x001]);
        }

        [TestMethod]
        public void DoubleAddToStorage_MswNegativeUnderflow()
        {
            // arrange
            Memory[0x0] = (0xC000).ToOnesCompliment(); // largest negative number;
            Memory[0x1] = (0xC000).ToOnesCompliment(); // largest negative number;

            Memory[0x200] = OnesCompliment.PositiveZero;
            Memory[0x201] = OnesCompliment.NegativeOne;

            Memory.LoadFixedRom(new ushort[] { 
                Instruction(0x02, 0x201)
            });

            // act
            CPU.Execute();

            // assert
            Assert.AreEqual(OnesCompliment.NegativeZero, Memory[0x200]);
            Assert.AreEqual(OnesCompliment.NegativeZero, Memory[0x201]);
            Assert.AreEqual(OnesCompliment.NegativeOne,  Memory[0x000]);
            Assert.AreEqual(OnesCompliment.PositiveZero, Memory[0x001]);
        }

        [TestMethod]
        public void DoubleAddToStorage_Positive1PlusNegative1()
        {
            // arrange
            Memory[0x0] = OnesCompliment.PositiveZero;
            Memory[0x1] = OnesCompliment.PositiveOne;

            Memory[0x200] = OnesCompliment.NegativeZero;
            Memory[0x201] = OnesCompliment.NegativeOne;

            Memory.LoadFixedRom(new ushort[] { 
                Instruction(0x02, 0x201)
            });

            // act
            CPU.Execute();

            // assert
            Assert.AreEqual(OnesCompliment.NegativeZero, Memory[0x200]);
            Assert.AreEqual(OnesCompliment.NegativeZero, Memory[0x201]);
            Assert.AreEqual(OnesCompliment.PositiveZero, Memory[0x000]);
            Assert.AreEqual(OnesCompliment.PositiveZero, Memory[0x001]);
        }

        [TestMethod]
        public void DoubleAddToStorage_DoublePrecisionDouble()
        {
            // arrange
            Memory[0x0] = OnesCompliment.PositiveZero;
            Memory[0x1] = OnesCompliment.PositiveOne;

            Memory.LoadFixedRom(new ushort[] {
                Instruction(0x02, 0x001)
            });

            // act
            CPU.Execute();

            // assert
            CustomAssert.AreEqual(OnesCompliment.PositiveZero, Memory[0x000]);
            CustomAssert.AreEqual(2, Memory[0x001]);
        }
    }
}
