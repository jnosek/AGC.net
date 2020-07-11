using Apollo.Virtual.AGC.Instructions;

namespace Apollo.Virtual.AGC
{
    static class StandardInstructions
    {
        public static InstructionList Build(Processor cpu) =>
            new InstructionList(new IInstruction[] {
                new TransferControl(cpu),
                new QuarterCodeInstructionList(new IQuarterCodeInstruction[] {
                    new DoubleAddToStorage(cpu),
                    new AddToStorage(cpu)
                }),
                new QuarterCodeInstructionList(new IQuarterCodeInstruction[]{
                    new TransferToStorage(cpu),
                    new DoubleExchange(cpu)
                }),
                new Add(cpu),
                new ClearAndAdd(cpu),
                new CountCompareAndSkip(cpu),
                new ClearAndSubtract(cpu)
            });
    }
}
