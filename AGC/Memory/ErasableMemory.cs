namespace Apollo.Virtual.AGC.Memory
{
    /// <summary>
    /// 15 bit memory location that is readwrite
    /// </summary>
    class ErasableMemory : MemoryWord
    {
        public ErasableMemory(ushort address, MemoryBank bank)
            : base(address, bank)
        {
        }
    }
}
