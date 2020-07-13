namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// DCOM
    /// DCS L - EX 0100_0000_0000_0001
    /// 
    /// The "Double Complement" bitwise complements the register pair A,L
    /// 
    /// All 16 bits of the accumulator and all 15 bits of the L register are complemented.  Therefore, in addition to negating the DP value (i.e., converting plus to minus and minus to plus), the overflow in the accumulator is preserved.
    /// </summary>
    /// <seealso cref="DoubleClearAndSubtract"/>
    public class DoubleCompliment
    {
        public const ushort Instruction = 0x4 << 12 | 0x1;
    }
}