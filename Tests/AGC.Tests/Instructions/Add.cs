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

            // insert add instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200,
                0x06000 | 0x201
            });

            // act - run the additions
            CPU.Execute();
            CPU.Execute();

            // assert
            Assert.AreEqual(25, Memory[0x0]);
        }

        [TestMethod]
        public void AddTwoNegativeNumbers()
        {
            // arrange
            Memory[0x200] = ToSP(-10);
            Memory[0x201] = ToSP(-15);

            // insert add instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200,
                0x06000 | 0x201
            });

            // act - run the additions
            CPU.Execute();
            CPU.Execute();

            // assert
            Assert.AreEqual(ToSP(-25), Memory[0x0]);
        }

        [TestMethod]
        public void AddPostive1AndNegative1()
        {
            // arrange
            Memory[0x200] = ToSP(-10);
            Memory[0x201] = 15;

            // insert add instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200,
                0x06000 | 0x201
            });

            // act - run the additions
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
            Memory[0x201] = ToSP(-1);

            // insert add instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200,
                0x06000 | 0x201
            });

            // act - run the additions
            CPU.Execute();
            CPU.Execute();

            //save acumulator value to a place in memory so we can view the overflow corrected value, for now
            Memory[0x202] = Memory[0x0];

            // assert
            Assert.AreEqual(NegativeZero, Memory[0x202]);
        }

        [TestMethod]
        public void AddTwoNumbersThatEqualPositiveZero()
        {
            // arrange
            Memory[0x200] = 0x3FFF; // most positive number 15 bit number
            Memory[0x201] = 1;

            // insert add instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200,
                0x06000 | 0x201
            });

            // act - run the additions
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
            Memory[0x201] = ToSP(-3);

            // insert add instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200,
                0x06000 | 0x201
            });

            // act - run the additions
            CPU.Execute();
            CPU.Execute();

            //save acumulator value to a place in memory so we can view the overflow corrected value, for now
            Memory[0x202] = Memory[0x0];

            // assert
            Assert.AreEqual(ToSP(-2), Memory[0x202]);
        }

        [TestMethod]
        public void AddTwoPositiveNumbersThatCauseOverflow()
        {
            // arrange
            Memory[0x200] = 0x3FFF; // most positive number 15 bit number
            Memory[0x201] = 3;

            // insert add instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200,
                0x06000 | 0x201
            });

            // act - run the additions
            CPU.Execute();
            CPU.Execute();

            //save acumulator value to a place in memory so we can view the overflow corrected value, for now
            Memory[0x202] = Memory[0x0];

            // assert
            Assert.AreEqual(2, Memory[0x202]);
        }
    }
}
