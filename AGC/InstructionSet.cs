using Apollo.Virtual.AGC.Instructions;

namespace Apollo.Virtual.AGC
{
    class InstructionSet : InstructionList
    {
        public InstructionSet()
            :base(7)
        {

            Add(new TransferControl());
            Add(new QuarterCode2());
            Add(new QuarterCode5());
            
            Add(new Add());
            Add(new ClearAndAdd());
            Add(new CountCompareAndSkip());
            Add(new ClearAndSubtract());
        }
    }
}
