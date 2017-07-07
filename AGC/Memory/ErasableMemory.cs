using Apollo.Virtual.AGC.Core;

namespace Apollo.Virtual.AGC.Memory
{
    /// <summary>
    /// 15 bit memory location that is readwrite
    /// </summary>
    class ErasableMemory : MemoryWord, IWord
    {
        public ErasableMemory(ushort address, MemoryBank bank)
            : base(address, bank)
        {
        }

        public void Write(ushort value)
        {
            var v = new OnesCompliment(value);
            v.OverflowCorrect();

            Set(v);
        }

        public ushort Read()
        {
            var v = new OnesCompliment(Get());
            v.SignExtend();

            return v;
        }
    }
}
