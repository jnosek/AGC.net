using Apollo.Virtual.AGC.Math;
using Apollo.Virtual.AGC.Memory;

namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// DV - EX 0001 00
    /// Quarter Code Instruction
    /// 
    /// The "Divide" instruction performs a division, giving a remainder and a quotient.
    /// 
    /// The double-length 1's-complement integer in the A,L register pair is divided by the 1's-complement integer in K, leaving the quotient in A and the remainder in L.  The integer K is required to be larger than the 1's-complement integer in A; this is natural since otherwise the quotient would be too large to fit into the A register.
    //  The signs of the dividend words stored in A and L do not necessarily agree with each other.
    /// </summary>
    public class Divide : IQuarterCodeInstruction
    {
        private const ushort _code = 0x1;
        private const ushort _quarterCode = 0x0;
        private const ushort _instruction = (_code << 12) | (_quarterCode << 10);

        public static ushort Encode(ushort address) => (ushort)(_instruction | address);

        public Divide(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        ushort IInstruction.Code => _code;

        ushort IQuarterCodeInstruction.QuarterCode => _quarterCode;

        void IInstruction.Execute(ushort K)
        {
            // A Register value is overflow corrected
            ushort dividendMsw = MemoryWord.OverflowCorrect(cpu.A.Read());
            ushort dividendLsw = cpu.L.Read();

            // get int value of dividend
            var dividend = new DoublePrecision(dividendMsw, dividendLsw).ToInt32();

            // if K is Q register, overflow correct the value
            var divisor = K == 0x2 ?
                MemoryWord.OverflowCorrect(cpu.Memory[K]) :
                cpu.Memory[K];
            
            var quotient = dividend / divisor;
            var remainder = dividend % divisor;
        }
    }
}
