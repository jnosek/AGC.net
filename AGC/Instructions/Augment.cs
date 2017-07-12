using Apollo.Virtual.AGC.Math;

namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// AUG - EX 0010 10
    /// Increments a positive value in erasable memory by 1
    /// Or decrements a negative value in erasable memory by -1
    /// </summary>
    class Augment: IInstruction
    {
        public ushort Code
        {
            get { return 0x02; }
        }

        public Processor CPU { get; set; }

        public void Execute(ushort K)
        {
            var value = CPU.Memory[K];

            // if negative
            if(value.IsNegative)
            {
                CPU.Memory[K] = value + OnesCompliment.NegativeOne;
            }
            // if positive
            else
            {
                CPU.Memory[K] = value + OnesCompliment.PositiveOne;
            }
        }
    }
}
