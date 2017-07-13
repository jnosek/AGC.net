namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// DIM - EX 0010 11
    /// Decrements a positive non-zero value in an erasable-memory location in-place,
    /// Or increments a negative non-zero value.
    /// </summary>
    class Diminish : IInstruction
    {
        public ushort Code
        {
            get { return 0x03; }
        }

        public Processor CPU { get; set; }

        public void Execute(ushort K)
        {
            var value = CPU.Memory[K];

            // if negative
            if(value.IsNegative)
            {
                CPU.Memory[K] = value + OnesCompliment.PositiveOne;
            }
            // is positive
            else
            {
                CPU.Memory[K] = value + OnesCompliment.NegativeOne;
            }
        }
    }
}
