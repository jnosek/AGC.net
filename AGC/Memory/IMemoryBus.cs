using Apollo.Virtual.AGC.Math;

namespace Apollo.Virtual.AGC.Memory
{
    public interface IMemoryBus
    {
        OnesCompliment this[ushort a] { get; set; }
        int MaxAddress { get; }

        /// <summary>
        /// The AGC has memory mapped registerss, so they need a method to regiser themselves to the MemoryBus
        /// </summary>
        /// <typeparam name="RegisterType"></typeparam>
        /// <param name="address"></param>
        /// <returns></returns>
        RegisterType MapRegister<RegisterType>(ushort address) where RegisterType : MemoryWord;
    }
}