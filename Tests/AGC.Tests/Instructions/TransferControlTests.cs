using Apollo.Virtual.AGC.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests.Instructions
{
    [TestClass]
    public class TransferControlTests : BaseTest
    {
        [TestMethod]
        public void TransferControl_Default()
        {
            // arrange

            // set Z, start execution from here
            Memory[0x5] = 0x202;

            // set instruction in memory
            Memory[0x202] = TransferControl.Encode(0x400);

            // act
            CPU.Execute();

            // assert
            Assert.AreEqual(0x203, Memory[0x2]); // test Q
            Assert.AreEqual(0x400, Memory[0x5]); // test Z
        }

        [TestMethod]
        public void TransferControl_Indirect()
        {
            // arrange
            // TODO: setup A using instruction in future test
            Memory[0x0] = 0x201; // set A to address for jump
            Memory[0x5] = 0x400; // set Z

            // act
            RunInstruction(TransferControl.Encode(0x000)); // Execute Jump

            // run one more cycle
            CPU.Execute();

            // assert
            Assert.AreEqual(0x001, Memory[0x2]); // test Q
            Assert.AreEqual(0x201, Memory[0x5]); // test Z
        }
    }
}
