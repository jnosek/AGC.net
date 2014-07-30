using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Base
{
    /// <summary>
    /// 16 bit memory bank
    /// </summary>
    public class MemoryBank
    {
        private ushort[] m;

        public MemoryBank(uint size)
        {
            m = new ushort[size];
        }

        public ushort this[uint address]
        {
            get
            {
                return m[address];
            }
            set
            {
                m[address] = value;
            }
        }

        internal void Copy(ushort[] data)
        {
            Array.Copy(data, m, data.Length);
        }
    }
}
