using Apollo.Virtual.AGC.Math;

namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// DAS - 0010 00
    /// QuarterCode Instruction
    /// 
    ///  A double-precision (DP) add of the A,L register pair to a pair of variables in erasable memory
    /// </summary>
    public class DoubleAddToStorage : IQuarterCodeInstruction
    {
        private const ushort _code = 0x2;
        private const ushort _quarterCode = 0x0;
        private const ushort _instruction = (_code << 12) | (_quarterCode << 10);

        public static ushort Encode(ushort address) => (ushort)(_instruction | address);

        public DoubleAddToStorage(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        ushort IInstruction.Code => _code;
        ushort IQuarterCodeInstruction.QuarterCode => _quarterCode;

        void IInstruction.Execute(ushort K0)
        {
            // find previous address
            var K1 = (ushort)(K0 - 1);

            // read DP values from registers and memroy
            var dp1 = new DoublePrecision(cpu.A.Read(), cpu.L.Read());
            var dp2 = new DoublePrecision(cpu.Memory[K1], cpu.Memory[K0]);

            // create sum
            var sum = dp1 + dp2;

            // calculate A and L result values first,
            // this handles the case for when K0 is the L register 

            // L always cleared to +0
            cpu.L.Write(OnesCompliment.PositiveZero);

            // A set based upon overflow
            if (sum.MostSignificantWord.IsPositiveOverflow)
                cpu.A.Write(OnesCompliment.PositiveOne);
            else if (sum.MostSignificantWord.IsNegativeOverflow)
                cpu.A.Write(OnesCompliment.NegativeOne);
            else
                cpu.A.Write(OnesCompliment.PositiveZero);

            // store result in memory
            cpu.Memory[K1] = sum.MostSignificantWord;
            cpu.Memory[K0] = sum.LeastSignificantWord;
        }
    }
}
