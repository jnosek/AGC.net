namespace Apollo.Virtual.AGC.Instructions
{
    interface IQuarterCodeInstruction : IInstruction
    {
        /// <summary>
        /// 2 bit additional Instruction Quarter Code
        /// </summary>
        /// <remarks>
        /// The instruction is composed of:
        /// 3 bit instruction code,
        /// 2 bit instruction quarter code
        /// 10 bit operand
        /// </remarks>
        ushort QuarterCode { get; }
    }
}
