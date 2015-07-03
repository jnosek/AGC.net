using Apollo.Virtual.AGC.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class Add : BaseTest
    {
        [TestMethod]
        public void AddTwoPositiveNumbers()
        {
            // arrange
            Memory[0x200] = 10;
            Memory[0x201] = 15;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200,
                0x06000 | 0x201
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            Assert.AreEqual(25, Memory[0x0]);
        }

        [TestMethod]
        public void AddTwoNegativeNumbers()
        {
            // arrange
            Memory[0x200] = (-10).ToOnesCompliment();
            Memory[0x201] = (-15).ToOnesCompliment();

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200,
                0x06000 | 0x201
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            Assert.AreEqual((-25).ToOnesCompliment(), Memory[0x0]);
        }

        [TestMethod]
        public void AddPostive1AndNegative1()
        {
            // arrange
            Memory[0x200] = (-10).ToOnesCompliment();
            Memory[0x201] = 15;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200,
                0x06000 | 0x201
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            Assert.AreEqual(5, Memory[0x0]);
        }

        [TestMethod]
        public void AddTwoNumbersThatEqualNegativeZero()
        {
            // arrange
            Memory[0x200] = 0xC000; // most negative number 15 bit number
            Memory[0x201] = OnesCompliment.NegativeOne;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200,
                0x06000 | 0x201
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            //save acumulator value to a place in memory so we can view the overflow corrected value, for now
            Memory[0x202] = Memory[0x0];

            // assert
            Assert.AreEqual(OnesCompliment.NegativeZero, Memory[0x202]);
        }

        [TestMethod]
        public void AddTwoNumbersThatEqualPositiveZero()
        {
            // arrange
            Memory[0x200] = 0x3FFF; // most positive number 15 bit number
            Memory[0x201] = 1;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200,
                0x06000 | 0x201
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            //save acumulator value to a place in memory so we can view the overflow corrected value, for now
            Memory[0x202] = Memory[0x0];

            // assert
            Assert.AreEqual(0, Memory[0x202]);
        }

        [TestMethod]
        public void AddTwoNegativeNumbersThatCauseUnderflow()
        {
            // arrange
            Memory[0x200] = 0xC000; // most negative number 15 bit number
            Memory[0x201] = (-3).ToOnesCompliment();

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200,
                0x06000 | 0x201
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            //save acumulator value to a place in memory so we can view the overflow corrected value, for now
            Memory[0x202] = Memory[0x0];

            // assert
            Assert.AreEqual((-2).ToOnesCompliment(), Memory[0x202]);
        }

        [TestMethod]
        public void AddTwoPositiveNumbersThatCauseOverflow()
        {
            // arrange
            Memory[0x200] = 0x3FFF; // most positive number 15 bit number
            Memory[0x201] = 3;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200,
                0x06000 | 0x201
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            //save acumulator value to a place in memory so we can view the overflow corrected value, for now
            Memory[0x202] = Memory[0x0];

            // assert
            Assert.AreEqual(2, Memory[0x202]);
        }

        [TestMethod]
        public void AddNegativeZeroAndPositiveNumber()
        {
            // arrange
            Memory[0x200] = OnesCompliment.NegativeZero;
            Memory[0x201] = 4;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200,
                0x06000 | 0x201
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            //save acumulator value to a place in memory so we can view the overflow corrected value, for now
            Memory[0x202] = Memory[0x0];

            // assert
            Assert.AreEqual(4, Memory[0x202]);
        }

        [TestMethod]
        public void AddNegativeZeroAndNegativeNumber()
        {
            // arrange
            Memory[0x200] = OnesCompliment.NegativeZero;
            Memory[0x201] = (-4).ToOnesCompliment();

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200,
                0x06000 | 0x201
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            //save acumulator value to a place in memory so we can view the overflow corrected value, for now
            Memory[0x202] = Memory[0x0];

            // assert
            Assert.AreEqual((-4).ToOnesCompliment(), Memory[0x202]);
        }

        [TestMethod]
        public void Add1AndNegative1EqualsNegative0()
        {
            // arrange
            Memory[0x200] = 0x01;
            Memory[0x201] = OnesCompliment.NegativeOne;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200,
                0x06000 | 0x201
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            //save acumulator value to a place in memory so we can view the overflow corrected value, for now
            Memory[0x202] = Memory[0x0];

            // assert
            Assert.AreEqual(OnesCompliment.NegativeZero, Memory[0x202]);
        }
    }
}
