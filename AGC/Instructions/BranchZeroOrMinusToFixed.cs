namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// BZMF - EX 0110
    /// 
    /// Jumps to a fixed memory location if the accumulator is 0 or negative
    /// </summary>
    class BranchZeroOrMinusToFixed : IInstruction
    {
        public ushort Code
        {
            get { return 0x06; }
        }

        public Processor CPU { get; set; }

        public void Execute(ushort K)
        {
            var value = CPU.A.Read();

            // if +0 or negative, jump
            if (value == 0 || (value & 0x8000) > 0)
                CPU.Z.Write(new OnesCompliment(K));
        }
    }
}
