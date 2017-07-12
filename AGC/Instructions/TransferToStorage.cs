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
    class TransferToStorage : IInstruction
    {
        public ushort Code
        {
            get { return 0x2; }
        }

        public Processor CPU { get; set; }

        public void Execute(ushort K)
        {
            var value = CPU.A.Read();

            CPU.Memory[K] = value;

            if(CPU.A.IsOverflow)
            {
                // test for 01-- ---- ---- ---- (positive overflow)
                if ((CPU.A.Read() & 0x4000) > 0)
                {
                    CPU.A.Write(OnesCompliment.PositiveOne);
                }
                // else negative overflow
                else
                {
                    CPU.A.Write(OnesCompliment.NegativeOne);
                }

                CPU.Z.Increment();
            }
        }
    }
}
