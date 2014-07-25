using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Base
{
    /// <summary>
    /// 16 bit memory segment
    /// </summary>
    class Memory
    {
        private ushort[] m;

        public Memory(uint size)
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
    }
}
