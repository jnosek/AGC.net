namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// AD - 0110
    /// 
    /// Adds the value located in K to the accumulator
    /// </summary>
    class Add : IInstruction
    {
        public Add(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        public ushort Code => 0x6;

        public void Execute(ushort K)
        {
            var value = cpu.Memory[K];

            // value in K is re-written
            // we do this first for the case of the DOUBLE instruction,
            // where K is the A register
            cpu.Memory[K] = value;

            cpu.A.Add(value);
        }
    }
}
