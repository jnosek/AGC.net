using Apollo.Virtual.AGC.Instructions;

namespace Apollo.Virtual.AGC
{
    static class ExtraInstructions
    {
       public static InstructionList Build(Processor cpu) =>
            new InstructionList(new IInstruction[]
            {
                new BranchZeroToFixed(cpu),
                new QuarterCodeInstructionList(new IQuarterCodeInstruction[] {
                    new Augment(cpu),
                    new Diminish(cpu)
                }),
                new QuarterCodeInstructionList(new IQuarterCodeInstruction[] {
                    new DoubleClearAndAdd(cpu)
                }),
                new QuarterCodeInstructionList(new IQuarterCodeInstruction[] {
                    new DoubleClearAndSubtract(cpu)
                }),
                new BranchZeroOrMinusToFixed(cpu)
            });
    }
}
