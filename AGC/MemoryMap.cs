using Apollo.Virtual.AGC.Core;
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
        /// Regisers to return at the following addresses
        /// 
        /// @0000 - @0061
        /// 0x0 - 0x31
        /// 0 - 49
        /// </summary>
        private IWord[] registers = new IWord[49];

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
            new MemoryBankOffset(256, 0x300),
            new MemoryBankOffset(256, 0x300),
            new MemoryBankOffset(256, 0x300),
            new MemoryBankOffset(256, 0x300),
            new MemoryBankOffset(256, 0x300),
            new MemoryBankOffset(256, 0x300),
            new MemoryBankOffset(256, 0x300),
            new MemoryBankOffset(256, 0x300),
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
            new MemoryBankOffset(1024, 0x400)
        };

        /// <summary>
        /// ROM
        /// 
        /// Directly addressable
        /// @4000-7777
        /// 0x800 - 0xFFF
        /// 2048 - 4095
        /// </summary>
        private MemoryBank fixedFixed = new MemoryBankOffset(2048, 0x800);

        /// <summary>
        /// Input and Output Channels
        /// Address space overlaps unswitched-ersable, but can only be access by IO instructions
        /// @000 - 777
        /// 0x0 - 0x1FF
        /// 0 - 511
        /// </summary>
        private MemoryBank ioChannels = new MemoryBank(512);

        public ushort this[ushort a]
        {
            get
            {
                return GetWord(a).Read();
            }
            set
            {
                GetWord(a).Write(value);
            }
        }

        public IWord GetWord(ushort address)
        {
            // registers
            if (address <= 0x031)
                return registers[address];

            int bank = 0;

            // look at bits in address to determine appropriate bank and memory type
            switch(address & 0xF00)
            {
                case 0x000:
                case 0x100:
                case 0x200:
                    return new ErasableMemory(address, unswitchedErasable);
                case 0x300:
                    // check value in EB to get the bank referenced (0x700 is bit mask)
                    bank = (unswitchedErasable[EB_Address] & 0x700) >> 8;

                    // retrieve bank, and adjust address by memory space offset
                    return new ErasableMemory(address, switchedErasable[bank]);
                case 0x400:
                case 0x500:
                case 0x600:
                case 0x700:
                    // check value in FB to get the bank referenced (0x7C00 is bit mask)
                    bank = (unswitchedErasable[FB_Address] & 0x7C00) >> 10;

                    // if we are in the super bit bank series
                    if (bank >= 32 && (ioChannels[7] & 0x40) > 0)
                        return new FixedMemory(address, commonFixed[bank + 0x08]);
                    else
                        return new FixedMemory(address, commonFixed[bank]);
                case 0x800:
                case 0x900:
                case 0xA00:
                case 0xB00:
                case 0xC00:
                case 0xD00:
                case 0xE00:
                case 0xF00:
                    return new FixedMemory(address, fixedFixed);
                default:
                    return new ErasableMemory(0x07, unswitchedErasable);
            }
        }

        /// <summary>
        /// Creates a given register type mapped to the unswitchedErasable bank and adds it to the memory space to be returned for their given address
        /// </summary>
        /// <typeparam name="RegisterType"></typeparam>
        /// <param name="address"></param>
        /// <returns></returns>
        internal RegisterType AddRegister<RegisterType>() where RegisterType : MemoryAddress, IWord
        {
            // reflectively get constructor that takes memory bank and call it
            var constructor = typeof(RegisterType).GetConstructor(new[] { typeof(MemoryBank) });

            var r = constructor.Invoke(new[] {unswitchedErasable}) as RegisterType;
            
            // add to memory space and return the object created
            registers[r.Address] = r;
            return r;
        }

        /// <summary>
        /// Creates a given register type mapped to the unswitchedErasable bank and adds it to the memory space to be returned for their given address
        /// </summary>
        /// <typeparam name="RegisterType"></typeparam>
        /// <param name="address"></param>
        /// <returns></returns>
        internal RegisterType AddRegister<RegisterType>(ushort address) where RegisterType : MemoryAddress, IWord
        {
            // reflectively get constructor that takes memory bank and call it
            var constructor = typeof(RegisterType).GetConstructor(new[] { typeof(ushort), typeof(MemoryBank) });

            var r = constructor.Invoke(new Object[] { address, unswitchedErasable }) as RegisterType;

            // add to memory space and return the object created
            registers[r.Address] = r;
            return r;
        }

        public void LoadFixedRom(ushort[] data)
        {
            fixedFixed.Copy(data);
        }
    }
}
