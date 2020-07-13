﻿using Apollo.Virtual.AGC.Instructions;
using Apollo.Virtual.AGC.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class DoubleClearAndSubtractTests : BaseTest
    {
        [TestMethod]
        public void DoubleClearAndSubstract_DoubleCompliment()
        {
            // arrange
            Memory[0x0] = (0x020).ToOnesCompliment();
            Memory[0x1] = (0x040).ToOnesCompliment();

            // act
            RunProgram(new[] {
                Extend.Instruction,
                DoubleCompliment.Instruction
            });

            // assert

            // check value in accumulator and L
            CustomAssert.AreEqual(0xFFDF, Memory[0x0]);
            CustomAssert.AreEqual(0xFFBF, Memory[0x1]);
        }

        [TestMethod]
        public void DoubleClearAndSubstract_Default()
        {
            // arrange
            Memory[0x200] = (0x020).ToOnesCompliment();
            Memory[0x201] = (0x040).ToOnesCompliment();

            // act
            RunProgram(new[] {
                Extend.Instruction,
                DoubleClearAndSubtract.Encode(0x201)
            });

            // assert

            // check value in accumulator and L
            CustomAssert.AreEqual(0xFFDF, Memory[0x0]);
            CustomAssert.AreEqual(0xFFBF, Memory[0x1]);
        }

        [TestMethod]
        public void DoubleClearAndSubstract_CycleRegister()
        {
            // arrange

            // writing int CYR will cause it to cycle right
            Memory[0x010] = (0x002).ToOnesCompliment();

            // writing into SR will cause it to shift right
            Memory[0x011] = (0x004).ToOnesCompliment();

            // act
            RunProgram(new[] {
                Extend.Instruction,
                DoubleClearAndSubtract.Encode(0x011)
            });

            // assert

            // check value in accumulator and L
            CustomAssert.AreEqual(0xFFFE, Memory[0x0]);
            CustomAssert.AreEqual(0xFFFD, Memory[0x1]);

            // check value in CYR was cycled
            CustomAssert.AreEqual(0xC000, Memory[0x10]);

            // check value in SR was shifted
            CustomAssert.AreEqual(0x0001, Memory[0x11]);
        }

        /// <summary>
        /// The instruction "DCS L" is an unusual case.   Since the less-significant word is processed first and then the more-significant word, the effect will be to first load the L register with the negative of the contents of the 16-bit Q register, and then to load the A register with the negative of the contents of L.In other words, A will be loaded with the contents of Q, and L will be loaded with the negative of the contents of Q.
        /// </summary>
        [TestMethod]
        public void DoubleClearAndSubstract_LRegister()
        {
            // arrange
            Memory[0x002] = (0x020).ToOnesCompliment();

            // act
            RunProgram(new[] {
                Extend.Instruction,
                DoubleClearAndSubtract.Encode(0x002)
            });

            // assert

            // check value in accumulator and L
            CustomAssert.AreEqual(0x0020, Memory[0x0]);
            CustomAssert.AreEqual(0xFFDF, Memory[0x1]);
        }

        /// <summary>
        ///  On the other hand, the instruction "DCS Q" will load A with the full 16-bit complement of Q, and will load L with the 15-bit complement of EB
        /// </summary>
        [TestMethod]
        public void DoubleClearAndSubstract_QRegister()
        {
            // arrange
            Memory[0x002] = (0xF000).ToOnesCompliment();
            Memory[0x003] = (0x0200).ToOnesCompliment();

            // act
            RunProgram(new[] {
                Extend.Instruction,
                DoubleClearAndSubtract.Encode(0x003)
            });

            // assert

            // check value in accumulator and L
            CustomAssert.AreEqual(0x0FFF, Memory[0x0]);
            CustomAssert.AreEqual(0xFDFF, Memory[0x1]);
        }
    }
}
