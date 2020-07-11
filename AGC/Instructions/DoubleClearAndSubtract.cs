namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// DCS - EX 0100 00
    /// 
    /// The "Double Clear and Subtract" instruction moves the 1's-complement (i.e., the negative) of the contents of a pair of memory locations into the A,L register pair.
    /// </summary>
    class DoubleClearAndSubtract : IQuarterCodeInstruction
    {
        public DoubleClearAndSubtract(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        public ushort Code => 0x4;
        public ushort QuarterCode => 0x0;

        public void Execute(ushort K0)
        {
            var K1 = (ushort)(K0 - 1);

            // compliment least significant word into L
            cpu.L.Write(~cpu.Memory[K0]);

            // rewrite K0
            cpu.Memory[K0] = cpu.Memory[K0];

            // compliment most significant word into A
            cpu.A.Write(~cpu.Memory[K1]);

            // rewrite K1
            cpu.Memory[K1] = cpu.Memory[K1];
        }
    }
}
