using Apollo.Virtual.AGC.Instructions;
using Apollo.Virtual.AGC.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class AddTests : BaseTest
    {
        private static readonly ushort[] baseProgram = new[] {
            Add.Encode(0x200),
            Add.Encode(0x201)
        };

        [TestMethod]
        public void Add_TwoPositiveNumbers()
        {
            // arrange
            Memory[0x200] = 10;
            Memory[0x201] = 15;

            // act - run the instructions
            RunProgram(baseProgram);

            // assert
            Assert.AreEqual(25, Memory[0x0]);
        }

        [TestMethod]
        public void Add_TwoNegativeNumbers()
        {
            // arrange
            Memory[0x200] = OnesCompliment.Convert(-10);
            Memory[0x201] = OnesCompliment.Convert(-15);

            // act - run the instructions
            RunProgram(baseProgram);

            // assert
            Assert.AreEqual(OnesCompliment.Convert(-25), Memory[0x0]);
        }

        [TestMethod]
        public void Add_Postive1AndNegative1()
        {
            // arrange
            Memory[0x200] = OnesCompliment.Convert(-10);
            Memory[0x201] = 15;

            // act - run the instructions
            RunProgram(baseProgram);

            // assert
            Assert.AreEqual(5, Memory[0x0]);
        }

        [TestMethod]
        public void Add_TwoNumbersThatEqualNegativeZero()
        {
            // arrange
            Memory[0x200] = 0xC000; // most negative number 15 bit number
            Memory[0x201] = OnesCompliment.NegativeOne;

            // act - run the instructions
            RunProgram(baseProgram);

            //save acumulator value to a place in memory so we can view the overflow corrected value, for now
            Memory[0x202] = Memory[0x0];

            // assert
            Assert.AreEqual(OnesCompliment.NegativeZero, Memory[0x202]);
        }

        [TestMethod]
        public void Add_TwoNumbersThatEqualPositiveZero()
        {
            // arrange
            Memory[0x200] = 0x3FFF; // most positive number 15 bit number
            Memory[0x201] = OnesCompliment.PositiveOne;

            // act - run the instructions
            RunProgram(baseProgram);

            //save acumulator value to a place in memory so we can view the overflow corrected value, for now
            Memory[0x202] = Memory[0x0];

            // assert
            Assert.AreEqual(OnesCompliment.PositiveZero, Memory[0x202]);
        }

        [TestMethod]
        public void Add_TwoNegativeNumbersThatCauseUnderflow()
        {
            // arrange
            Memory[0x200] = 0xC000; // most negative number 15 bit number
            Memory[0x201] = OnesCompliment.Convert(-3);

            // act - run the instructions
            RunProgram(baseProgram);

            //save acumulator value to a place in memory so we can view the overflow corrected value, for now
            Memory[0x202] = Memory[0x0];

            // assert
            Assert.AreEqual(OnesCompliment.Convert(-2), Memory[0x202]);
        }

        [TestMethod]
        public void Add_TwoPositiveNumbersThatCauseOverflow()
        {
            // arrange
            Memory[0x200] = 0x3FFF; // most positive number 15 bit number
            Memory[0x201] = 3;

            // act - run the instructions
            RunProgram(baseProgram);

            //save acumulator value to a place in memory so we can view the overflow corrected value, for now
            Memory[0x202] = Memory[0x0];

            // assert
            Assert.AreEqual(2, Memory[0x202]);
        }

        [TestMethod]
        public void Add_NegativeZeroAndPositiveNumber()
        {
            // arrange
            Memory[0x200] = OnesCompliment.NegativeZero;
            Memory[0x201] = 4;

            // act - run the instructions
            RunProgram(baseProgram);

            //save acumulator value to a place in memory so we can view the overflow corrected value, for now
            Memory[0x202] = Memory[0x0];

            // assert
            Assert.AreEqual(4, Memory[0x202]);
        }

        [TestMethod]
        public void Add_NegativeZeroAndNegativeNumber()
        {
            // arrange
            Memory[0x200] = OnesCompliment.NegativeZero;
            Memory[0x201] = OnesCompliment.Convert(-4);

            // act - run the instructions
            RunProgram(baseProgram);

            //save acumulator value to a place in memory so we can view the overflow corrected value, for now
            Memory[0x202] = Memory[0x0];

            // assert
            Assert.AreEqual(OnesCompliment.Convert(-4), Memory[0x202]);
        }

        [TestMethod]
        public void Add_1AndNegative1EqualsNegative0()
        {
            // arrange
            Memory[0x200] = OnesCompliment.PositiveOne;
            Memory[0x201] = OnesCompliment.NegativeOne;

            // act - run the instructions
            RunProgram(baseProgram);

            //save acumulator value to a place in memory so we can view the overflow corrected value, for now
            Memory[0x202] = Memory[0x0];

            // assert
            Assert.AreEqual(OnesCompliment.NegativeZero, Memory[0x202]);
        }

        [TestMethod]
        public void Add_Double()
        {
            // arrange
            Memory[0x000] = 2;

            // act - run the instructions
            RunInstruction(Double.Instruction);

            // assert
            Assert.AreEqual(4, Memory[0x000]);
        }
    }
}
