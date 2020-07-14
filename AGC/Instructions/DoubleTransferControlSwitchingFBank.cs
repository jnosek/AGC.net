namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// DTCF
    /// DXCH Z 0101_0100_0000_0101
    /// 
    /// The "Double Transfer Control, Switching F Bank" instruction performs a jump to a different fixed memory bank, by simultaneously loading the FB and Z registers.
    /// 
    /// This instruction exchanges the contents of A with FB, and the contents of L with Z.Thus by preloading the A,L register pair, we can effectively perform a jump to a different fixed-memory bank, whilst preserving the current address and fixed-memory bank for a later return.
    /// </summary>
    /// <seealso cref="DoubleExchange"/>
    public class DoubleTransferControlSwitchingFBank
    {
        public const ushort Instruction =
            (0x5 << 12) | // code
            (0x1 << 10) | // quarter code
            0x5; // Z register address
    }
}