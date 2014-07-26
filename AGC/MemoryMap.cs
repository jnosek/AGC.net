using Apollo.Virtual.AGC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC
{
    public class MemoryMap
    {
        const uint EB_Address = 0x03;
        const uint FB_Address = 0x04;

        /// <summary>
        /// Primary RAM
        /// 
        /// @0000 - @1377
        /// 0x0 - 0x2FF
        /// 0 - 767
        /// </summary>
        private MemoryBank unswitchedErasable = new MemoryBank(768);

        /// <summary>
        /// RAM
        /// 
        /// @1400 - @1777
        /// 0x300 - 0x3FF
        /// 768 - 1023
        /// 
        /// Divided into 8 (E0 - E7) banks controlled by register EB or BB
        /// </summary>
        private MemoryBank[] switchedErasable = new MemoryBank[] 
        {
            new MemoryBank(256),
            new MemoryBank(256),
            new MemoryBank(256),
            new MemoryBank(256),
            new MemoryBank(256),
            new MemoryBank(256),
            new MemoryBank(256),
            new MemoryBank(256)
        };

        /// <summary>
        /// ROM
        /// 
        /// @2000 - @3777
        /// 0x400 - 0x7FF
        /// 1024 - 2047
        /// 
        /// Divided into 32 banks controlled by register FB or BB, and the super-bank bit in i/o channel 7 (FEB)
        /// </summary>
        private MemoryBank[] commonFixed = new MemoryBank[]
        {
            new MemoryBank(1024)
        };

        /// <summary>
        /// ROM
        /// 
        /// Directly addressable
        /// @4000-7777
        /// 0x800 - 0xFFF
        /// 2048 - 4095
        /// </summary>
        private MemoryBank fixedFixed = new MemoryBank(2048);

        /// <summary>
        /// Input and Output Channels
        /// Address space overlaps unswitched-ersable, but can only be access by IO instructions
        /// @000 - 777
        /// 0x0 - 0x1FF
        /// 0 - 511
        /// </summary>
        private MemoryBank ioChannels = new MemoryBank(512);

        public ushort this[uint a]
        {
            get
            {
                var address = GetAddress(a);
                return address.Read();
            }
            set
            {
                var address = GetAddress(a);
                address.Write(value);
            }
        }

        public MemoryAddress GetAddress(uint address)
        {
            //unswitchedErasable
            if (address <= 0x2FF)
            {
                return new MemoryAddress(unswitchedErasable, address);
            }
            // switchedErasable
            else if (address <= 0x3FF)
            {
                // check value in EB to get the bank referenced (0x700 is bit mask)
                var bank = (unswitchedErasable[EB_Address] & 0x700) >> 8;

                // retrieve bank, and adjust address by memory space offset
                return new MemoryAddress(switchedErasable[bank], address - 0x300);
            }
            // commonFixed
            else if (address <= 0x7FF)
            {
                // check value in FB to get the bank referenced (0x7C00 is bit mask)
                var bank = (unswitchedErasable[FB_Address] & 0x7C00) >> 10;

                // if we are in the super bit bank series
                if(bank >= 32 && (ioChannels[7] & 0x40) > 0 )
                    return new MemoryAddress(commonFixed[bank + 0x08], address - 0x400);
                else
                    return new MemoryAddress(commonFixed[bank], address - 0x400);
            }
            // for now, return the 0 space address
            else 
                return new MemoryAddress(unswitchedErasable, 0x07);
        }
    }
}
