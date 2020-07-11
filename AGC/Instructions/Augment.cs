using Apollo.Virtual.AGC.Math;

namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// AUG - EX 0010 10
    /// Increments a positive value in erasable memory by 1
    /// Or decrements a negative value in erasable memory by -1
    /// </summary>
    class Augment: IQuarterCodeInstruction
    {
        public Augment(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        public ushort Code => 0x2;
        public ushort QuarterCode => 0x2;

        public void Execute(ushort K)
        {
            var value = cpu.Memory[K];

            // if negative
            if(value.IsNegative)
            {
                cpu.Memory[K] = value + OnesCompliment.NegativeOne;
            }
            // if positive
            else
            {
                cpu.Memory[K] = value + OnesCompliment.PositiveOne;
            }
        }
    }
}
