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
        const ushort EXTEND = 0x06;

        public ushort Code
        {
            get { return 0x00; }
        }

        public Processor CPU { get; set; }

        public void Execute(ushort K)
        {
            // if this is an extend instruction, set the extra code flag
            if (K == EXTEND)
            {
                CPU.ExtraCodeFlag = true;
            }
            // else process as a TC command
            else
            {
                // set Q to the next instruction, for when we return
                CPU.Q.Write(CPU.Z.Read());

                // set control to K
                CPU.Z.Write(new OnesCompliment(K));

                // clear the extra code flag
                CPU.ExtraCodeFlag = false;
            }
        }
    }
}
