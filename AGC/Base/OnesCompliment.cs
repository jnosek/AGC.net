using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Base
{
    public static class OnesCompliment
    {
        public const ushort NegativeZero = 0xFFFF;
        public const ushort PositiveOne = 0x0001;
        public const ushort NegativeOne = 0xFFFE;

        /// <summary>
        /// Mainly used for converting 2's compliment negative numbers into Ones Compliment values
        /// </summary>
        /// <param name="value">2's Compliment Value (normal .net value)</param>
        /// <returns>Ones Compliment coded value</returns>
        public static ushort ToOnesCompliment(this int value)
        {
            // if this is negative, 
            // return the 14 lower bits of the 1's compliment of the positive value 
            // (this is just subtracting 1 from the value), 
            // and append a 1 at bit-15
            if (value < 0)
            {
                return (ushort)(0x4000 | (value - 1));
            }
            // else return the positive value
            else
                return (ushort)value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static ushort Add(this ushort left, ushort right)
        {
            var sum = left + right;

            // if we have overflow, most likely from subtracting negative numbers
            if ((sum & 0x10000) > 0)
            {
                // we need to Single Precision correct the negative number by adding 1 and taking the lower 16 bits
                sum = sum + 1;
                sum = sum & 0xFFFF;
            }

            return (ushort)sum;
        }

        /// <summary>
        /// Performs overflow correction on a 16bit value, converting it to a 15 bit value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ushort OverflowCorrect(this ushort value)
        {
            uint newValue = value;

            // get lower 14 bits
            uint lowerBits = newValue & 0x3FFF;

            // move 16-th bit, into 15th position, isolate it, and set it in above value;
            newValue = (newValue >> 1 & 0x4000) | lowerBits;

            return (ushort)newValue;
        }

        /// <summary>
        /// Performs sign extending on a 15bit value, converting it to a 16 bit value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ushort SignExtend(this ushort value)
        {
            uint newValue = value;

            // take lower 15-bits
            newValue = newValue & 0x7FFF;

            // shift left 1 and take 16th bit, combine with lower 15 bits
            newValue = ((newValue << 1) & 0x8000) | newValue;

            return (ushort)newValue;
        }
    }
}
