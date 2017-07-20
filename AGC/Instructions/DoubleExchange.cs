namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// DXCH - 0101 01
    /// Exchanges the double-precision (DP) value in the register-pair A,L 
    /// with a value stored in the erasable memory variable pair K,K+1.
    /// </summary>
    class DoubleExchange : IInstruction
    {
        public ushort Code
        {
            get { return 0x01; }
        }

        public Processor CPU { get; set; }

        public void Execute(ushort K0)
        {
            // find previous address
            var K1 = (ushort)(K0 - 1);

            // swap L
            var k0Value = CPU.Memory[K0];
            CPU.Memory[K0] = CPU.L.Read();
            CPU.L.Write(k0Value);

            // swap A 
            var k1Value = CPU.Memory[K1];
            CPU.Memory[K1] = CPU.A.Read();

            // if K1 is L, then K0 is Q
            // Q is a full 16 bits, that was transfered into L and needs to be tranfered into A
            // it cannot be the 15-bit corrected value since A and Q are full 16 bits
            // so use k0Value
            if(K1 == 0x1)
            {
                CPU.A.Write(k0Value);
            }
            // else just write regular value
            else
            {
                CPU.A.Write(k1Value);
            }
        }
    }
}
