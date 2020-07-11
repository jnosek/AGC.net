using Apollo.Virtual.AGC.Math;
using Apollo.Virtual.AGC.Memory;

namespace Apollo.Virtual.AGC.Registers
{
    class BothBanksRegister : MemoryWord
    {
        public BothBanksRegister(ushort address, MemoryBank bank) 
            : base(address, bank)
        {
        }

        public override void Write(OnesCompliment value)
        {
            // mask values, since they are the only ones that can be set
            var eb = value.NativeValue & 0x0007;
            var fb = value.NativeValue & 0x7C00;

            // write values to eb and fb register
            // shift eb values left by 8
            WriteRaw(eb << 8, 0x3);
            WriteRaw(fb, 0x4);

            // write combined, masked value
            WriteRaw(eb | fb);
        }
    }
}
