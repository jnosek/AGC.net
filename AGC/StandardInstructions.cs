using Apollo.Virtual.AGC.Instructions;

namespace Apollo.Virtual.AGC
{
    static class StandardInstructions
    {
        public static InstructionList Build(Processor cpu) =>
            new InstructionList(new IInstruction[] {
                new TransferControl(cpu),
                new QuarterCodeInstructionList(0x02, new IInstruction[] {
                    new DoubleAddToStorage(cpu),
                    new AddToStorage(cpu)
                }),
                new QuarterCodeInstructionList(0x05, new IInstruction[]{
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
