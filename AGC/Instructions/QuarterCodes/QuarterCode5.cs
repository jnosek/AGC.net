namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// QC - 0101
    /// 
    /// Operation on 10-bit address space operand restricted to eraseable memory
    /// Followed by 2-bit quarter code for instruction
    /// </summary>
    class QuarterCode5 : QuarterCodeInstructionList
    {
        public QuarterCode5()
        {
            Add(new TransferToStorage());
            Add(new DoubleExchange());
        }

        public override ushort Code
        {
            get { return 0x05; }
        }
    }
}
