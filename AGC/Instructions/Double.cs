namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// DOUBLE
    /// AD A - 0110_0000_0000_0000
    /// 
    /// The "Double the Contents of A" instruction adds the accumulator to itself.
    /// 
    /// The value in the accumulator is added to itself, then placed back into the accumulator.
    /// Note that if the accumulator contains overflow prior the addition, the accumulator will not be overflow-corrected 
    /// prior to the addition, and thus the sign of the resulting sum will not be correct.
    /// </summary>
    /// <seealso cref="Add"/>
    public class Double
    {
        public const ushort Instruction = 0x6 << 12;
    }
}
