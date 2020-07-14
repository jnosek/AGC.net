using Apollo.Virtual.AGC.Math;
using System.Diagnostics;

namespace Apollo.Virtual.AGC.Memory
{
    /// <summary>
    /// 15 bit memory location that is readonly
    /// </summary>
    class FixedMemory : MemoryWord
    {
        public FixedMemory(ushort address, MemoryBank bank)
            : base(address, bank)
        {
        }

        /// <summary>
        /// cannot write to fixed memory
        /// </summary>
        /// <param name="value"></param>
        public override void Write(ushort value)
        {
        }
    }
}
