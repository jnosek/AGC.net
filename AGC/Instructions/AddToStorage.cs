namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// ADS - 0010 11
    /// QuaterCode Instruction
    /// 
    /// Adds the accumulator to an eraseable memory location and vice versa 
    /// </summary>
    public class AddToStorage : IQuarterCodeInstruction
    {
        private const ushort _code = 0x2;
        private const ushort _quarterCode = 0x3;
        private const ushort _instruction = (_code << 12) | (_quarterCode << 10);

        public static ushort Encode(ushort address) => (ushort)(_instruction | address);

        public AddToStorage(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        ushort IInstruction.Code => _code;
        ushort IQuarterCodeInstruction.QuarterCode => _quarterCode;

        void IInstruction.Execute(ushort K)
        {
            var value = cpu.Memory[K];
            cpu.A.Add(value);

            cpu.Memory[K] = cpu.A.Read();
        }
    }
}
