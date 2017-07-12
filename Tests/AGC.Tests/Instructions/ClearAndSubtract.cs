using Apollo.Virtual.AGC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class ClearAndSubtract : BaseTest
    {
        [TestMethod]
        public void ClearAndSubtract_Accumulator()
        {
            // arrange
            Memory[0x0] = (0x0101).ToOnesCompliment();

            Memory.LoadFixedRom(new ushort[] {
                0x4000
            });

            // act
            CPU.Execute();

            // assert

            CustomAssert.AreEqual(0xFEFE, Memory[0x0]);
        }

        [TestMethod]
        public void ClearAndSubtract_Memory()
        {
            // arrange
            Memory[0x200] = (0xF0F0).ToOnesCompliment();

            Memory.LoadFixedRom(new ushort[] {
                0x4000 | 0x0200
            });
            
            // act
            CPU.Execute();

            // assert
            CustomAssert.AreEqual(0x0F0F, Memory[0x0]);
        }
    }
}
