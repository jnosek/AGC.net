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

        public override void Write(ushort value)
        {
            // mask values, since they are the only ones that can be set
            var eb = value & 0x0007;
            var fb = value & 0x7C00;

            // write values to eb and fb register
            // shift eb values left by 8
            UnmodifiedWrite(eb << 8, 0x3);
            UnmodifiedWrite(fb, 0x4);

            // write combined, masked value
            UnmodifiedWrite(eb | fb);
        }
    }
}
