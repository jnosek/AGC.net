namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// TC - 0000
    /// 
    /// Calls a subroutine and prepares for return to the following instruction
    /// 
    /// Also functions as EXTEND command when K is 0x06
    /// </summary>
    public class TransferControl : IInstruction
    {
        private const ushort _code = 0x0;
        private const ushort _instruction = _code << 12;

        public static ushort Encode(ushort address) => (ushort)(_instruction | address);

        public TransferControl(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        ushort IInstruction.Code => _code;

        void IInstruction.Execute(ushort K)
        {
            // if this is an extend instruction, set the extra code flag
            if (K == Extend.Instruction)
            {
                cpu.ExtraCodeFlag = true;
            }
            // else process as a TC command
            else
            {
                // set Q to the next instruction, for when we return
                cpu.Q.Write(cpu.Z.Read());

                // set control to K
                cpu.Z.Write(K);

                // clear the extra code flag
                cpu.ExtraCodeFlag = false;
            }
        }
    }
}
