namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// EQC - 0011
    /// 
    /// 10-bit memory instructions in the extra code set
    /// </summary>
    class ExtraQuarterCode3 : QuarterCodeInstructionList
    {
        public ExtraQuarterCode3()
        {
            Add(new DoubleClearAndAdd());
        }

        public override ushort Code
        {
            get { return 0x03; }
        }
    }
}
