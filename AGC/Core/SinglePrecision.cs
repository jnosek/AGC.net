using Apollo.Virtual.AGC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Core
{
    public class SinglePrecision : OnesCompliment
    {
        public SinglePrecision(ushort v) 
            : base(v)
        {
        }

        public SinglePrecision(int v)
            : base(v)
        {
        }

        public bool IsPositiveOverflow
        {
            get
            {
                return (Value & 0xC000) == 0x4000;
            }
        }

        public bool IsNegativeOverflow
        {
            get
            {
                return (Value & 0xC000) == 0x8000;
            }
        }

        /// <summary>
        /// Single Precision Addition
        /// </summary>
        /// <param name="left">left operand</param>
        /// <param name="right">right operand</param>
        /// <returns></returns>
        public static SinglePrecision operator +(SinglePrecision left, OnesCompliment right)
        {
            var sum = left.Value + right.Value;

            // if we have overflow, most likely from subtracting negative numbers
            if ((sum & 0x10000) > 0)
            {
                // we need to ones compliment correct the negative number by adding 1 and taking the lower 16 bits
                // this process is called "end around carry"
                sum = sum + 1;
                sum = sum & 0xFFFF;
            }

            return new SinglePrecision(sum);
        }

        public static SinglePrecision operator +(SinglePrecision left, ushort right)
        {
            return left + new SinglePrecision(right);
        }

        public static SinglePrecision operator ~(SinglePrecision a)
        {
            return new SinglePrecision((ushort)~a.Value);
        }
    }
}
