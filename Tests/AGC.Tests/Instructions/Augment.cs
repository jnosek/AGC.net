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
    public class Augment : BaseTest
    {
        [TestMethod]
        public void AugmentPositiveNumber()
        {
            // arrange
            Memory[0x200] = 10;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x02800 | 0x200
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            Assert.AreEqual(11, Memory[0x200]);
        }

        [TestMethod]
        public void AugmentNegativeNumber()
        {
            // arrange
            Memory[0x200] = (-10).ToOnesCompliment();

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x00006, // EXTEND instruction
                0x02800 | 0x200
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            Assert.AreEqual((-11).ToOnesCompliment(), Memory[0x200]);
        }
    }
}
