namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// TS - 0101 10
    /// QuaterCode Instruction
    /// 
    /// Copies the accumulator into memory location K
    /// 
    /// Additionally if the accumulator has overflow, 
    ///     it is loaded with +1 for positive overflow
    ///     or -1 for negative overflow
    ///     and the program counter is advanced again
    /// </summary>
    class TransferToStorage : IQuarterCodeInstruction
    {
        public TransferToStorage(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        public ushort Code => 0x5;
        public ushort QuarterCode => 0x2;

        public void Execute(ushort K)
        {
            var value = cpu.A.Read();

            cpu.Memory[K] = value;

            if(cpu.A.IsOverflow)
            {
                // test for 01-- ---- ---- ---- (positive overflow)
                if ((cpu.A.Read() & 0x4000) > 0)
                {
                    cpu.A.Write(OnesCompliment.PositiveOne);
                }
                // else negative overflow
                else
                {
                    cpu.A.Write(OnesCompliment.NegativeOne);
                }

                cpu.Z.Increment();
            }
        }
    }
}
