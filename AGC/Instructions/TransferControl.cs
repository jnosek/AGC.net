namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// TC - 0000
    /// 
    /// Calls a subroutine and prepares for return to the following instruction
    /// 
    /// Also functions as EXTEND command when K is 0x06
    /// </summary>
    class TransferControl : IInstruction
    {
        public TransferControl(Processor cpu)
        {
            this.cpu = cpu;
        }

        const ushort EXTEND = 0x06;

        private readonly Processor cpu;

        public ushort Code => 0x00;

        public void Execute(ushort K)
        {
            // if this is an extend instruction, set the extra code flag
            if (K == EXTEND)
            {
                cpu.ExtraCodeFlag = true;
            }
            // else process as a TC command
            else
            {
                // set Q to the next instruction, for when we return
                cpu.Q.Write(cpu.Z.Read());

                // set control to K
                cpu.Z.Write(new OnesCompliment(K));

                // clear the extra code flag
                cpu.ExtraCodeFlag = false;
            }
        }
    }
}
