using System.Collections.Generic;

namespace Apollo.Virtual.AGC.Instructions
{
    class QuarterCodeInstructionList : InstructionList, IInstruction
    {
        public QuarterCodeInstructionList(
            ushort quarterCode,
            ICollection<IInstruction> instructions)
            : base(Convert(instructions))
        {
            Code = quarterCode;
        }

        public ushort Code { get; }

        public void Execute(ushort K)
        {
            var quarterCode = ConvertCode(K);
            K = (ushort)(K & 0x3FF);

            base[quarterCode].Execute(K);
        }

        private static ushort ConvertCode(ushort instruction) =>
            (ushort)(instruction >> 1 & 0x00_3);

        private static IDictionary<ushort, IInstruction> Convert(ICollection<IInstruction> instructions)
        {
            var convertedInstructions = new Dictionary<ushort, IInstruction>();

            foreach(var instruction in instructions)
            {
                var convertedCode = ConvertCode(instruction.Code);
                convertedInstructions.Add(convertedCode, instruction);
            }

            return convertedInstructions;
        }
    }
}
