namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// EXTEND - 0000_0000_0000_0110
    /// 
    /// Set the Extracode flag, so that the next instruction 
    /// encountered is taken from the "extracode" instruction 
    /// set rather than from the "basic" instruction set.
    /// </summary>
    /// <remarks>
    /// A special variation of the TransferControl instructions
    /// </remarks>
    /// <seealso cref="TransferControl"/>
    public class Extend
    {
        public const ushort Instruction = 0x0006;
    }
}
