using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGC.Tests.Registers
{
    [TestClass]
    public class CycleRightRegister : BaseTest
    {
        [TestMethod]
        public void CycleRightOneWrapAround()
        {
            // arrange
            Memory[0x200] = 51;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200, // add to the accumulator
                0x05800 | 0x010  // transfer to storage, the CYR register
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            Assert.AreEqual(0XC019, Memory[0x10]);
        }

        [TestMethod]
        public void CycleRightZeroWrapAround()
        {
            // arrange
            Memory[0x200] = 50;

            // insert instructions
            Memory.LoadFixedRom(new ushort[] {
                0x06000 | 0x200, // add to the accumulator
                0x05800 | 0x010  // transfer to storage, the CYR register
            });

            // act - run the instructions
            CPU.Execute();
            CPU.Execute();

            // assert
            Assert.AreEqual(0X19, Memory[0x10]);
        }
    }
}
