using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Base
{
    public static class SinglePrecision
    {
        public const ushort NegativeZero = 0xFFFF;

        /// <summary>
        /// Mainly used for converting 2's compliment negative numbers into Single Precision values
        /// </summary>
        /// <param name="value">2's Compliment Value (normal .net value)</param>
        /// <returns>Single Precision coded value</returns>
        public static ushort To(short value)
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
        public static ushort Add(ushort left, ushort right)
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
    }
}
