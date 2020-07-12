using Apollo.Virtual.AGC.Math;

namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// DIM - EX 0010 11
    /// Decrements a positive non-zero value in an erasable-memory location in-place,
    /// Or increments a negative non-zero value.
    /// </summary>
    public class Diminish : IQuarterCodeInstruction
    {
        private const ushort _code = 0x2;
        private const ushort _quarterCode = 0x3; 
        private const ushort _instruction = (_code << 12) | (_quarterCode << 10);

        public static ushort Encode(ushort address) => (ushort)(_instruction | address);

        public Diminish(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        ushort IInstruction.Code => _code;
        ushort IQuarterCodeInstruction.QuarterCode => _quarterCode;

        void IInstruction.Execute(ushort K)
        {
            var value = cpu.Memory[K];

            // if negative
            if(value.IsNegative)
            {
                cpu.Memory[K] = value + OnesCompliment.PositiveOne;
            }
            // is positive
            else
            {
                cpu.Memory[K] = value + OnesCompliment.NegativeOne;
            }
        }
    }
}
