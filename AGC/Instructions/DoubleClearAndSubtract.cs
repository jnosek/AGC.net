namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// DCS - EX 0100 00
    /// 
    /// The "Double Clear and Subtract" instruction moves the 1's-complement (i.e., the negative) of the contents of a pair of memory locations into the A,L register pair.
    /// </summary>
    public class DoubleClearAndSubtract : IQuarterCodeInstruction
    {
        private const ushort _code = 0x4;
        private const ushort _quarterCode = 0x0;
        private const ushort _instruction = (_code << 12) | (_quarterCode << 10);

        public static ushort Encode(ushort address) => (ushort)(_instruction | address);

        public DoubleClearAndSubtract(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        ushort IInstruction.Code => _code;
        ushort IQuarterCodeInstruction.QuarterCode => _quarterCode;

        void IInstruction.Execute(ushort K0)
        {
            var K1 = (ushort)(K0 - 1);

            // compliment least significant word into L
            cpu.L.Write((ushort)~cpu.Memory[K0]);

            // rewrite K0
            cpu.Memory[K0] = cpu.Memory[K0];

            // compliment most significant word into A
            cpu.A.Write((ushort)~cpu.Memory[K1]);

            // rewrite K1
            cpu.Memory[K1] = cpu.Memory[K1];
        }
    }
}
