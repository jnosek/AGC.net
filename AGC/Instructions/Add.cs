namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// AD - 0110
    /// 
    /// Adds the value located in K to the accumulator
    /// </summary>
    class Add : IInstruction
    {
        public Processor CPU { get; set; }

        public ushort Code { get { return 0x06; } }

        public void Execute(ushort K)
        {
            var value = CPU.Memory[K];

            // value in K is re-written
            // we do this first for the case of the DOUBLE instruction,
            // where K is the A register
            CPU.Memory[K] = value;

            CPU.A.Add(value);
        }
    }
}
