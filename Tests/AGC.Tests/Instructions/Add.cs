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
    }
}
