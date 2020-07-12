namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// CA - 0011
    /// 
    /// Moves the contents of memory at location K into the accumulator
    /// </summary>
    public class ClearAndAdd : IInstruction
    {
        public const ushort _code = 0x3;
        private const ushort _instruction = _code << 12;

        public static ushort Encode(ushort address) => (ushort)(_instruction | address);

        public ClearAndAdd(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        ushort IInstruction.Code => _code;

        void IInstruction.Execute(ushort K)
        {
            var value = cpu.Memory[K];

            // set value in accumulator
            cpu.A.Write(value);

            // value in K is re-written
            cpu.Memory[K] = value;
        }
    }
}
