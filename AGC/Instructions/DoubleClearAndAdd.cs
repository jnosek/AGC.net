namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// DCA - EX 0011 00
    /// 
    /// The "Double Clear and Add" instruction moves the contents of a pair of memory locations into the A,L register pair.
    /// </summary>
    class DoubleClearAndAdd : IInstruction
    {
        public ushort Code
        {
            get { return 0x0; }
        }

        public Processor CPU { get; set; }

        public void Execute(ushort K0)
        {
            /// FYI, all double instructions are encoded with the address of the second word (K0, need to subtract 1 to find K1)
            var K1 = (ushort)(K0 - 1);

            // move least significant word into L
            CPU.L.Write(CPU.Memory[K0]);

            // rewrite K0
            CPU.Memory[K0] = CPU.Memory[K0];

            // move most significant word into A
            CPU.A.Write(CPU.Memory[K1]);

            // rewrite K1
            CPU.Memory[K1] = CPU.Memory[K1];
        }
    }
}
