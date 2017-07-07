using Apollo.Virtual.AGC.Instructions;

namespace Apollo.Virtual.AGC
{
    class ExtraCodeInstructionSet : InstructionList
    {
        public ExtraCodeInstructionSet()
            : base(7)
        {
            Add(new BranchZeroToFixed());
            Add(new ExtraQuarterCode2());
            Add(new ExtraQuarterCode3());
            Add(new ExtraQuarterCode4());
            Add(new BranchZeroOrMinusToFixed());
        }
    }
}
