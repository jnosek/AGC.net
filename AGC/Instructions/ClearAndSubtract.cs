namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// CS - 0100
    /// 
    /// The "Clear and Subtract" instruction moves the 1's-complement (i.e., the negative) of a memory location into the accumulator.
    /// 
    /// Also:
    /// COM - 0100 0000 0000 0000
    /// The "Complement the Contents of A" bitwise complements the accumulator
    /// Assembles as CS A
    /// </summary>
    public class ClearAndSubtract : IInstruction
    {
        private const ushort _code = 0x4;
        private const ushort _instruction = _code << 12;

        public static ushort Encode(ushort address) => (ushort)(_instruction | address);

        public ClearAndSubtract(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        ushort IInstruction.Code => _code;

        void IInstruction.Execute(ushort K)
        {
            var value = cpu.Memory[K];

            // write the compliment to the accumulator
            cpu.A.Write((ushort)~value);

            // if not the A register, re-write value to K
            if (K != cpu.A.Address)
            {
                cpu.Memory[K] = value;
            }
        }
    }
}
