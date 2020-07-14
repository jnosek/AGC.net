namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// DDOUBL
    /// DAS L - 0010_0000_0000_0001
    /// 
    /// The "Double Precision Double" instruction adds the double-precision (DP) value in the A,L register pair to itself.
    /// 
    /// The A,L register pair is treated as a DP value, and is added to itself, returning a DP value in the A,L register.
    /// Note that if the accumulator contains overflow prior the addition, the accumulator will not be overflow-corrected prior to the addition, and thus the sign of the resulting sum will not be correct.As Blair-Smith and Savage&Drake state, the results will be "messy".
    /// </summary>
    /// <seealso cref="DoubleAddToStorage"/>
    public class DoublePrecisionDouble
    {
        public const ushort Instruction = 0x2 << 12 | 0x1;
    }
}