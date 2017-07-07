namespace Apollo.Virtual.AGC.Architecture.Instructions
{
    /// <summary>
    /// DCS - EX 0100 00
    /// 
    /// The "Double Clear and Subtract" instruction moves the 1's-complement (i.e., the negative) of the contents of a pair of memory locations into the A,L register pair.
    /// </summary>
    class DoubleClearAndSubtract : IInstruction
    {
        public ushort Code
        {
            get { return 0x0; }
        }

        public Processor CPU { get; set; }

        public void Execute(ushort K0)
        {
            var K1 = (ushort)(K0 - 1);

            // compliment least significant word into L
            CPU.L.Write((ushort)~CPU.Memory[K0]);

            // rewrite K0
            CPU.Memory[K0] = CPU.Memory[K0];

            // compliment most significant word into A
            CPU.A.Write((ushort)~CPU.Memory[K1]);

            // rewrite K1
            CPU.Memory[K1] = CPU.Memory[K1];
        }
    }
}
