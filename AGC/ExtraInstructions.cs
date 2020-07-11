using Apollo.Virtual.AGC;
using Apollo.Virtual.AGC.Instructions;

namespace AGC
{
    static class ExtraInstructions
    {
       public static InstructionList Build(Processor cpu) =>
            new InstructionList(new IInstruction[]
            {
                new BranchZeroToFixed(cpu),
                new QuarterCodeInstructionList(0x02, new IInstruction[] {
                    new Augment(cpu),
                    new Diminish(cpu)
                }),
                new QuarterCodeInstructionList(0x02, new IInstruction[] {
                    new DoubleClearAndAdd(cpu)
                }),
                new QuarterCodeInstructionList(0x04, new IInstruction[] {
                    new DoubleClearAndSubtract(cpu)
                }),
                new BranchZeroOrMinusToFixed(cpu)
            });
    }
}
