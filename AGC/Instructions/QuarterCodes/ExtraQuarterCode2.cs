namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// EQC - EX 0010
    /// 
    /// 10-bit memory instructions in the extra code set
    /// </summary>
    class ExtraQuarterCode2 : QuarterCodeInstructionList
    {
        public ExtraQuarterCode2()
        {
            Add(new Augment());
            Add(new Diminish());
        }

        public override ushort Code
        {
            get { return 0x02; }
        }
    }
}
