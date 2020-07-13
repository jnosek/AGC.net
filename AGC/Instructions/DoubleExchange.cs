namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// DXCH - 0101 01
    /// 
    /// Exchanges the double-precision (DP) value in the register-pair A,L 
    /// with a value stored in the erasable memory variable pair K,K+1.
    /// </summary>
    public class DoubleExchange : IQuarterCodeInstruction
    {
        private const ushort _code = 0x5;
        private const ushort _quarterCode = 0x1;
        private const ushort _instruction = (_code << 12) | (_quarterCode << 10);

        public static ushort Encode(ushort address) => (ushort)(_instruction | address);

        public DoubleExchange(Processor cpu)
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

            // swap L
            var k0Value = cpu.Memory[K0];
            cpu.Memory[K0] = cpu.L.Read();
            cpu.L.Write(k0Value);

            // swap A 
            var k1Value = cpu.Memory[K1];
            cpu.Memory[K1] = cpu.A.Read();

            // if K1 is L, then K0 is Q
            // Q is a full 16 bits, that was transfered into L and needs to be tranfered into A
            // it cannot be the 15-bit corrected value since A and Q are full 16 bits
            // so use k0Value
            if(K1 == 0x1)
            {
                cpu.A.Write(k0Value);
            }
            // else just write regular value
            else
            {
                cpu.A.Write(k1Value);
            }
        }
    }
}
