namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// AD - 0110
    /// 
    /// Adds the value located in K to the accumulator
    /// </summary>
    public class Add : IInstruction
    {
        private const ushort _code = 0x6;
        private const ushort _instruction = _code << 12;

        public static ushort Encode(ushort address) => (ushort)(_instruction | address);
        
        public Add(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        ushort IInstruction.Code => _code;

        void IInstruction.Execute(ushort K)
        {
            var value = cpu.Memory[K];

            // value in K is re-written
            // we do this first for the case of the DOUBLE instruction,
            // where K is the A register
            cpu.Memory[K] = value;

            cpu.A.Add(value);
        }
    }
}
