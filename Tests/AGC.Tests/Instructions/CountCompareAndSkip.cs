﻿using Apollo.Virtual.AGC.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class CountCompareAndSkip : BaseTest
    {
        [TestMethod]
        public void CountCompareSkip_PositiveNumber()
        {
            // arrange
            Memory[0x200] = 10;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                Instruction(0x01, 0x200)
            });

            // act
            CPU.Execute();

            // assert

            // check that number was decremented
            Assert.AreEqual(9, Memory[0x0]);

            // check that program counter advanced 1
            Assert.AreEqual(0x801, Memory[0x05]);
        }

        [TestMethod]
        public void CountCompareSkip_PositiveZero()
        {
            // arrange
            Memory[0x200] = 0;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                Instruction(0x01, 0x200)
            });

            // act
            CPU.Execute();

            // assert

            // check that number was not decremented
            Assert.AreEqual(0, Memory[0x0]);

            // check that program counter advanced 2
            Assert.AreEqual(0x802, Memory[0x05]);
        }

        [TestMethod]
        public void CountCompareSkip_NegativeZero()
        {
            // arrange
            Memory[0x200] = OnesCompliment.NegativeZero;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                Instruction(0x01, 0x200)
            });

            // act
            CPU.Execute();

            // assert

            // check that number was set to positive 0
            Assert.AreEqual(0, Memory[0x0]);

            // check that program counter advanced 4
            Assert.AreEqual(0x804, Memory[0x05]);
        }

        [TestMethod]
        public void CountCompareSkip_Negative()
        {
            // arrange
            Memory[0x200] = (-10).ToOnesCompliment();

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                Instruction(0x01, 0x200)
            });

            // act
            CPU.Execute();

            // assert

            // check that number ABS - 1
            Assert.AreEqual(9, Memory[0x0]);

            // check that program counter advanced 3
            Assert.AreEqual(0x803, Memory[0x05]);
        }

        // TODO: added +0 and -0 tests
    }
}
