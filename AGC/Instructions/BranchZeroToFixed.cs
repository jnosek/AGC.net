namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// BZF - EX 0001
    /// 
    /// Jumps to a fixed memory location if the accumulator is 0
    /// </summary>
    class BranchZeroToFixed : IInstruction
    {
        public BranchZeroToFixed(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        public ushort Code => 0x1;

        public void Execute(ushort K)
        {
            // if in overflow, no jump
            if(cpu.A.IsOverflow)
                return;
            
            var value = cpu.A.Read();

            // if +0 or -0, then jump
            if (value == 0 || value == OnesCompliment.NegativeZero)
                cpu.Z.Write(new OnesCompliment(K));
        }
    }
}
