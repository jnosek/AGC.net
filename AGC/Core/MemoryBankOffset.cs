using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Core
{
    class MemoryBankOffset : MemoryBank
    {
        private uint offset;

        public MemoryBankOffset(uint size, uint offset)
            : base(size)
        {
            this.offset = offset;
        }

        public override ushort this[uint address]
        {
            get
            {
                return base[address - offset];
            }
            set
            {
                base[address - offset] = value;
            }
        }
    }
}
