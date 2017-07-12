using Apollo.Virtual.AGC.Math;

namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// DAS - 0010 00
    /// QuarterCode Instruction
    /// 
    ///  A double-precision (DP) add of the A,L register pair to a pair of variables in erasable memory
    /// </summary>
    class DoubleAddToStorage : IInstruction
    {
        public ushort Code
        {
            get { return 0x00; }
        }

        public Processor CPU { get; set; }

        public void Execute(ushort K0)
        {
            // TODO: need to handle DDOUBL

            // find previous address
            var K1 = (ushort)(K0 - 1);
            
            // read DP values from registers and memroy
            var dp1 = new DoublePrecision(CPU.A.Read(), CPU.L.Read());
            var dp2 = new DoublePrecision(CPU.Memory[K1], CPU.Memory[K0]);

            // create sum
            var sum = dp1 + dp2;

            // store result in memory
            CPU.Memory[K1] = sum.MostSignificantWord;
            CPU.Memory[K0] = sum.LeastSignificantWord;

            // L always cleared to +0
            CPU.L.Write(OnesCompliment.PositiveZero);

            // A set based upon overflow
            if(sum.MostSignificantWord.IsPositiveOverflow)
                CPU.A.Write(OnesCompliment.PositiveOne);
            else if(sum.MostSignificantWord.IsNegativeOverflow)
                CPU.A.Write(OnesCompliment.NegativeOne);
            else
                CPU.A.Write(OnesCompliment.PositiveZero);
        }
    }
}
