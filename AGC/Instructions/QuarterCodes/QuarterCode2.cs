namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// QC - 0010
    /// 
    /// Operation on 10-bit address space operand restricted to eraseable memory
    /// Followed by 2-bit quarter code for instruction
    /// </summary>
    class QuarterCode2 : QuarterCodeInstructionList
    {
        public QuarterCode2()
        {
            Add(new DoubleAddToStorage());
            Add(new AddToStorage());
        }

        public override ushort Code
        {
            get { return 0x02; }
        }
    }
}
