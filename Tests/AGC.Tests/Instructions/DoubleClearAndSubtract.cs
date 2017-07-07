using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class DoubleClearAndSubtract : BaseTest
    {
        [TestMethod]
        public void DoubleClearAndSubstract_Default()
        {
            // arrange
            Memory[0x200] = 0x020;
            Memory[0x201] = 0x040;

            // insert instructions and test data
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x04000 | 0x201,
            });

            // act
            CPU.Execute();
            CPU.Execute();

            // assert

            // check value in accumulator and L
            Assert.AreEqual(0xFFDF, Memory[0x0]);
            Assert.AreEqual(0xFFBF, Memory[0x1]);
        }

        [TestMethod]
        public void DoubleClearAndSubstract_CycleRegister()
        {
            // arrange

            // writing int CYR will cause it to cycle right
            Memory[0x010] = 0x002;

            // writing into SR will cause it to shift right
            Memory[0x011] = 0x004;

            // insert instructions and test data
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x04000 | 0x011, // SR address
            });

            // act
            CPU.Execute();
            CPU.Execute();

            // assert

            // check value in accumulator and L
            Assert.AreEqual(0xFFFE, Memory[0x0]);
            Assert.AreEqual(0xFFFD, Memory[0x1]);

            // check value in CYR was cycled
            Assert.AreEqual(0xC000, Memory[0x10]);

            // check value in SR was shifted
            Assert.AreEqual(0x0001, Memory[0x11]);
        }

        /// <summary>
        /// The instruction "DCS L" is an unusual case.   Since the less-significant word is processed first and then the more-significant word, the effect will be to first load the L register with the negative of the contents of the 16-bit Q register, and then to load the A register with the negative of the contents of L.In other words, A will be loaded with the contents of Q, and L will be loaded with the negative of the contents of Q.
        /// </summary>
        [TestMethod]
        public void DoubleClearAndSubstract_LRegister()
        {
            // arrange
            Memory[0x002] = 0x020;

            // insert instructions and test data
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x04000 | 0x002, // Q address
            });

            // act
            CPU.Execute();
            CPU.Execute();

            // assert

            // check value in accumulator and L
            Assert.AreEqual(0x0020, Memory[0x0]);
            Assert.AreEqual(0xFFDF, Memory[0x1]);
        }

        /// <summary>
        ///  On the other hand, the instruction "DCS Q" will load A with the full 16-bit complement of Q, and will load L with the 15-bit complement of EB
        /// </summary>
        [TestMethod]
        public void DoubleClearAndSubstract_QRegister()
        {
            // arrange
            Memory[0x002] = 0xF000;
            Memory[0x003] = 0x0200;

            // insert instructions and test data
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x04000 | 0x003, // EB address
            });

            // act
            CPU.Execute();
            CPU.Execute();

            // assert

            // check value in accumulator and L
            Assert.AreEqual(0x0FFF, Memory[0x0]);
            Assert.AreEqual(0xFDFF, Memory[0x1]);
        }
    }
}
