namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// DCTB
    /// DXCH BB 0101_0100_0000_0110
    /// 
    /// The "Double Transfer Control, Switching Both Banks" instruction performs a jump and switching both fixed and erasable banks, by simultaneously loading the BB and Z registers.
    /// 
    /// This instruction exchanges the contents of A with Z, and the contents of L with B.  Thus by preloading the A,L register pair, we can effectively perform a jump to a different memory bank, and switching both fixed and erasable banks, whilst preserving the current address and erasable- and fixed-memory banks for a later return.
    /// </summary>
    /// <seealso cref="DoubleExchange"/>
    public class DoubleTransferControlSwitchingBothBanks
    {
        public const ushort Instruction =
            (0x5 << 12) | // code
            (0x1 << 10) | // quarter code
            0x6; // BB register address
    }
}