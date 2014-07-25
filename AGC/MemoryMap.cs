using Apollo.Virtual.AGC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC
{
    class MemoryMap
    {
        /// <summary>
        /// @0000 - @1377
        /// 0x0 - 0x2FF
        /// 0 - 767
        /// </summary>
        private Memory unswitchedErasable = new Memory(768);

        /// <summary>
        /// @1400 - @1777
        /// 0x300 - 0x3FF
        /// 768 - 1023
        /// 
        /// Divided into 8 (E0 - E7) banks controlled by register EB or BB
        /// </summary>
        private Memory[] switchedErasable = new Memory[] 
        {
            new Memory(256),
            new Memory(256),
            new Memory(256),
            new Memory(256),
            new Memory(256),
            new Memory(256),
            new Memory(256),
            new Memory(256),
        };

        /// <summary>
        /// ROM
        /// @2000 - @3777
        /// 0x400 - 0x7FF
        /// 1024 - 2047
        /// 
        /// Divided into 32 banks controlled by register FB or BB, and the super-bank bit in i/o channel 7 (FEB)
        /// </summary>
        private Memory[] commonFixed = new Memory[]
        {
            new Memory(1024)
        };

        /// <summary>
        /// ROM
        /// Directly addressable
        /// @4000-7777
        /// 0x800 - 0xFFF
        /// 2048 - 4095
        /// </summary>
        private Memory fixedFixed = new Memory(2048);

        /// <summary>
        /// Input and Output Channels
        /// Address space overlaps unswitched-ersable, but can only be access by IO instructions
        /// @000 - 777
        /// 0x0 - 0x1FF
        /// 0 - 511
        /// </summary>
        private Memory ioChannels = new Memory(512);
    }
}
