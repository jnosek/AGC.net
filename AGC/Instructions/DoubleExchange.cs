namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// DXCH - 0101 01
    /// Exchanges the double-precision (DP) value in the register-pair A,L 
    /// with a value stored in the erasable memory variable pair K,K+1.
    /// </summary>
    class DoubleExchange : IInstruction
    {
        public DoubleExchange(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        public ushort Code => 0x00_1;

        public void Execute(ushort K0)
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
