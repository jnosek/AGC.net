namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// BZMF - EX 0110
    /// 
    /// Jumps to a fixed memory location if the accumulator is 0 or negative
    /// </summary>
    class BranchZeroOrMinusToFixed : IInstruction
    {
        public BranchZeroOrMinusToFixed(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        public ushort Code => 0x6;

        public void Execute(ushort K)
        {
            var value = cpu.A.Read();

            // if +0 or negative, jump
            if (value == 0 || (value & 0x8000) > 0)
                cpu.Z.Write(new OnesCompliment(K));
        }
    }
}
