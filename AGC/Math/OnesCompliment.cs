using System;
using System.Xml.Schema;

namespace Apollo.Virtual.AGC.Math
{
    /// <summary>
    /// This is the base numeric system used by the computer 
    /// (it does not use Twos-Compliment like x86 computers, so conversion methods are needed)
    /// </summary>
    public static class OnesCompliment
    {
        public const ushort NegativeZero = 0xFFFF;
        public const ushort PositiveZero = 0x0000;
        public const ushort PositiveOne = 0x0001;
        public const ushort NegativeOne = 0xFFFE;
        public const ushort SignMask = 0xC000;

        public static bool IsNegativeZero(ushort value) => value == NegativeZero;

        public static bool IsPositiveZero(ushort value) => value == 0;

        public static bool IsZero(ushort value) => value == 0 || value == NegativeZero;

        public static bool IsNegative(ushort value) => (value & 0x8000) > 0;

        public static bool IsPositive(ushort value) => (value & 0x8000) == 0;
        
        public static bool IsPositiveOverflow(ushort value) => (value & SignMask) == 0x4000;
        
        public static bool IsNegativeOverflow(ushort value) => (value & SignMask) == 0x8000;
        
        public static ushort Add(ushort left, ushort right)
        {
            var sum = left + right;

            // if we have overflow, most likely from subtracting negative numbers
            if ((sum & 0x10000) > 0)
            {
                // we need to ones compliment correct the negative number by adding 1 and taking the lower 16 bits
                // this process is called "end around carry"
                sum += 1;
                sum &= 0xFFFF;
            }

            return (ushort)sum;
        }

        public static ushort AddPositiveOne(ushort value) => Add(value, PositiveOne);

        public static ushort AddNegativeOne(ushort value) => Add(value, NegativeOne);

        /// <summary>
        /// The diminished absolute value is defined as DABS(x)=|x|-1 if |x|>1, or +0 otherwise
        /// </summary>
        public static ushort GetDiminishedAbsoluteValue(ushort value)
        {
            // if negative, NOT 1's to get ABS
            ushort abs = IsNegative(value) ? (ushort)~value : value;

            if (IsPositive(abs) && abs != PositiveZero)
                return AddNegativeOne(abs);
            else
                return PositiveZero;
        }

        /// <summary>
        /// Converts a ushort one's compliment number to a native 
        /// int two's compliment
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ConvertToNative(ushort value)
        {
            if (IsZero(value))
                return 0;
            else if (IsNegative(value))
                // compliment to get positive number, then negate
                return -(~value);
            else
                return (int)value;
        }

        /// <summary>
        /// Converstion helper, mainly to handle negative values
        /// </summary>
        /// <remarks>
        /// Make the negative int positive, and then compliment the binary value
        /// </remarks>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ushort Convert(int value) => (ushort)(value < 0 ? ~(-value): value);
    }
}
