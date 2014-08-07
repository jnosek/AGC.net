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
    public class AddToStorage : BaseTest
    {
        [TestMethod]
        public void AddToStorageTwoPositiveNumbers()
        {
            // arrange
            Memory[0x000] = 10;
            Memory[0x201] = 15;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x02C00 | 0x201
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            Assert.AreEqual(25, Memory[0x0]);
            Assert.AreEqual(25, Memory[0x201]);
        }

        [TestMethod]
        public void AddToStorageTwoNegativeNumbers()
        {
            // arrange
            Memory[0x000] = SinglePrecision.To(-10);
            Memory[0x201] = SinglePrecision.To(-15);

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x02C00 | 0x201
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            Assert.AreEqual(SinglePrecision.To(-25), Memory[0x0]);
            Assert.AreEqual(SinglePrecision.To(-25), Memory[0x201]);
        }
    }
}
