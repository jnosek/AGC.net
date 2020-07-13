using Apollo.Virtual.AGC.Math;
using System.Xml.Schema;

namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// AUG - EX 0010 10
    /// Increments a positive value in erasable memory by 1
    /// Or decrements a negative value in erasable memory by -1
    /// </summary>
    public class Augment: IQuarterCodeInstruction
    {
        private const ushort _code = 0x2;
        private const ushort _quarterCode = 0x2;
        private const ushort _instruction = (_code << 12) | (_quarterCode << 10);

        public static ushort Encode(ushort address) => (ushort)(_instruction | address);

        public Augment(Processor cpu)
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
            if(OnesCompliment.IsNegative(value))
            {
                cpu.Memory[K] = OnesCompliment.AddNegativeOne(value);
            }
            // if positive
            else
            {
                cpu.Memory[K] = OnesCompliment.AddPositiveOne(value);
            }
        }
    }
}
