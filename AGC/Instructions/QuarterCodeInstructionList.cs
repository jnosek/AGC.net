using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Apollo.Virtual.AGC.Instructions
{
    class QuarterCodeInstructionList : InstructionList, IInstruction
    {
        public QuarterCodeInstructionList(
            IEnumerable<IQuarterCodeInstruction> instructions)
            : base(instructions)
        {
            Code = instructions.First().Code;

            // ensure all instructions use the same code
            Debug.Assert(instructions.All(s => s.Code == Code));
        }

        public ushort Code { get; }

        public void Execute(ushort K)
        {
            var quarterCode = DecodeQuarterCode(K);
            K = (ushort)(K & 0x3FF);

            base[quarterCode].Execute(K);
        }

        private static ushort DecodeQuarterCode(ushort instruction) =>
            (ushort)(instruction >> 10 & 0x3);
    }
}
