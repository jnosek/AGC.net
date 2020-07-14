using Apollo.Virtual.AGC.Math;
using System.Diagnostics;
using System.Threading.Tasks;
﻿using System.Diagnostics;

namespace Apollo.Virtual.AGC.Memory
{
    /// <summary>
    /// Default 15-bit ones compliment memory word
    /// </summary>
    [DebuggerDisplay("{Read()}")]
    public class MemoryWord : IWord
    {
        private MemoryBank bank;

        public ushort Address { get; }

        public MemoryWord(ushort address, MemoryBank bank)
        {
            this.bank = bank;
            Address = address;
        }

        public virtual ushort Read()
        {
            return bank[Address];
        }

        /// <summary>
        /// read a value at a specific address within the same memory bank
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        protected ushort Read(ushort address)
        {
            return bank[address];
        }

        /// <summary>
        /// 15-bit writes are overflow corrected from 16-bit values
        /// </summary>
        /// <param name="value"></param>
        public virtual void Write(ushort value)
        {
            // TODO: see if this logic can be simplified
            // and the two operations can be condensed

            // in case there is an overflow, correct it
            var correctedValue = OverflowCorrect(value);

            // sign extend, since we store all values as 16-bit
            correctedValue = SignExtend(correctedValue);

            bank[Address] = correctedValue;
        }

        /// <summary>
        /// Write value into bank without overflow correction or sign extension
        /// </summary>
        /// <param name="value"></param>
        protected void UnmodifiedWrite(ushort value)
        {
            bank[Address] = value;
        }

        // <summary>
        /// Write value into bank without overflow correction or sign extension
        /// </summary>
        /// <param name="value"></param>
        protected void UnmodifiedWrite(int value)
        {
            bank[Address] = (ushort)value;
        }

        /// <summary>
        /// write a value at a specific address within the same memory bank
        /// without overflow correction or sign extension
        /// </summary>
        /// <param name="value"></param>
        /// <param name="address"></param>
        protected void UnmodifiedWrite(int value, ushort address)
        {
            bank[address] = (ushort)value;
        }

        protected static ushort OverflowCorrect(ushort value)
        {
            uint correctedValue = value;

            // get lower 14 bits
            uint lowerBits = correctedValue & 0x3FFF;

            // move 16-th bit, into 15th position, isolate it, and set it in above value;
            correctedValue = (correctedValue >> 1 & 0x4000) | lowerBits;

            return (ushort)correctedValue;
        }

        /// <summary>
        /// Performs sign extending on a 15bit value, converting it to a 16 bit value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static ushort SignExtend(ushort value)
        {
            uint extendedValue = value;

            // take lower 15-bits
            extendedValue &= 0x7FFF;

            // shift left 1 and take 16th bit, combine with lower 15 bits
            extendedValue = (extendedValue << 1 & 0x8000) | extendedValue;

            return (ushort)extendedValue;
        }
    }
}
