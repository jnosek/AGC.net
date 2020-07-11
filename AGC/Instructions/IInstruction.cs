namespace Apollo.Virtual.AGC.Instructions
{
    interface IInstruction
    {
        /// <summary>
        /// 4 bit instruction code
        /// </summary>
        /// <remarks>
        /// The instruction is composed of:
        /// 3 bit instruction code, 
        /// 12 bit operand
        /// </remarks>
        ushort Code { get; }
        
        void Execute(ushort K);
    }
}
