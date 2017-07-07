namespace Apollo.Virtual.AGC.Instructions
{
    class ExtraQuarterCode4 : QuarterCodeInstructionList
    {
        public ExtraQuarterCode4()
        {
            Add(new DoubleClearAndSubtract());
        }

        public override ushort Code
        {
            get { return 0x04; }
        }
    }
}
