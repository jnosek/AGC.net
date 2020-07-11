namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// DCA - EX 0011 00
    /// 
    /// The "Double Clear and Add" instruction moves the contents of a pair of memory locations into the A,L register pair.
    /// </summary>
    class DoubleClearAndAdd : IInstruction
    {
        public DoubleClearAndAdd(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        public ushort Code => 0x00_0;

        public void Execute(ushort K0)
        {
            /// FYI, all double instructions are encoded with the address of the second word (K0, need to subtract 1 to find K1)
            var K1 = (ushort)(K0 - 1);

            // move least significant word into L
            cpu.L.Write(cpu.Memory[K0]);

            // rewrite K0
            cpu.Memory[K0] = cpu.Memory[K0];

            // move most significant word into A
            cpu.A.Write(cpu.Memory[K1]);

            // rewrite K1
            cpu.Memory[K1] = cpu.Memory[K1];
        }
    }
}
