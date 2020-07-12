namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// CS A - 0100_0000_0000_0000
    /// 
    /// The "Complement the Contents of A" bitwise complements the accumulator
    /// 
    /// All 16 bits of the accumulator are complemented.  
    /// Therefore, in addition to negating the contents of the register 
    /// (i.e., converting plus to minus and minus to plus), the overflow 
    /// is preserved, but swtiches type (i.e., negative overflow will 
    /// become positive overflow).
    /// </summary>
    /// <seealso cref="ClearAndSubtract"/>
    public class Compliment
    {
        public const ushort Instruction = 0x4 << 12;
    }
}
