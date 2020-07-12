using Apollo.Virtual.AGC.Math;

namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// BZMF - EX 0110
    /// 
    /// Jumps to a fixed memory location if the accumulator is 0 or negative
    /// </summary>
    public class BranchZeroOrMinusToFixed : IInstruction
    {
        private const ushort _code = 0x6;
        private const ushort _instruction = _code << 12;

        public static ushort Encode(ushort address) => (ushort)(_instruction | address);

        public BranchZeroOrMinusToFixed(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        ushort IInstruction.Code => _code;

        void IInstruction.Execute(ushort K)
        {
            var value = cpu.A.Read();

            // if +0 or negative, jump
            if (value == 0 || (value & 0x8000) > 0)
                cpu.Z.Write(new OnesCompliment(K));
        }
    }
}
