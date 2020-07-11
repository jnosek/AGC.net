namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// CA - 0011
    /// 
    /// Moves the contents of memory at location K into the accumulator
    /// </summary>
    class ClearAndAdd : IInstruction
    {
        public ClearAndAdd(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        public ushort Code => 0x3;

        public void Execute(ushort K)
        {
            var value = cpu.Memory[K];

            // set value in accumulator
            cpu.A.Write(value);

            // value in K is re-written
            cpu.Memory[K] = value;
        }
    }
}
