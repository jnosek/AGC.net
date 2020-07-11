namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// ADS - 0010 11
    /// QuaterCode Instruction
    /// 
    /// Adds the accumulator to an eraseable memory location and vice versa 
    /// </summary>
    class AddToStorage : IQuarterCodeInstruction
    {
        public AddToStorage(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        public ushort Code => 0x2;
        public ushort QuarterCode => 0x3;

        public void Execute(ushort K)
        {
            var value = cpu.Memory[K];
            cpu.A.Add(value);

            cpu.Memory[K] = cpu.A.Read();
        }
    }
}
