using Apollo.Virtual.AGC.Math;

namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// BZF - EX 0001
    /// 
    /// Jumps to a fixed memory location if the accumulator is 0
    /// </summary>
    public class BranchZeroToFixed : IInstruction
    {
        private const ushort _code = 0x1;
        private const ushort _instruction = _code << 12;

        public static ushort Encode(ushort address) => (ushort)(_instruction | address);

        public BranchZeroToFixed(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        ushort IInstruction.Code => _code;

        void IInstruction.Execute(ushort K)
        {
            // if in overflow, no jump
            if(cpu.A.IsOverflow)
                return;
            
            var value = cpu.A.Read();

            // if +0 or -0, then jump
            if (value == 0 || value == OnesCompliment.NegativeZero)
                cpu.Z.Write(K);
        }
    }
}
