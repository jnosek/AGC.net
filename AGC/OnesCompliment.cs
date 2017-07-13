using System.Diagnostics;

namespace Apollo.Virtual.AGC
{
    /// <summary>
    /// This is the base numeric system used by the computer 
    /// (it does not use Twos-Compliment like x86 computers, so a conversion layer is needed)
    /// </summary>
    [DebuggerDisplay("{NativeValue}")]
    public struct OnesCompliment
    {
        public static readonly OnesCompliment NegativeZero = new OnesCompliment(0xFFFF);
        public static readonly OnesCompliment PositiveZero = new OnesCompliment(0x0000);
        public static readonly OnesCompliment PositiveOne = new OnesCompliment(0x0001);
        public static readonly OnesCompliment NegativeOne = new OnesCompliment(0xFFFE);

        public ushort NativeValue { get; private set; }

        public OnesCompliment(ushort value)
        {
            NativeValue = value;
        }

        public OnesCompliment(int value)
        {
            NativeValue = (ushort)value;
        }

        public bool IsNegativeZero {
            get
            {
                return NativeValue == NegativeZero.NativeValue;
            }
        }

        public bool IsPositiveZero
        {
            get
            {
                return NativeValue == 0;
            }
        }

        public bool IsNegative
        {
            get
            {
                return (NativeValue & 0x8000) > 0;
            }
        }

        public bool IsPositive
        {
            get
            {
                return (NativeValue & 0x8000) == 0;
            }
        }

        public bool IsPositiveOverflow
        {
            get
            {
                return (NativeValue & 0xC000) == 0x4000;
            }
        }

        public bool IsNegativeOverflow
        {
            get
            {
                return (NativeValue & 0xC000) == 0x8000;
            }
        }


        public static int operator &(OnesCompliment left, ushort right)
        {
            return left.NativeValue & right;
        }

        public static int operator |(OnesCompliment left, ushort right)
        {
            return left.NativeValue | right;
        }

        public static OnesCompliment operator ~(OnesCompliment right)
        {
            return new OnesCompliment(~right.NativeValue);
        }

        //public static OnesCompliment operator +(OnesCompliment left, ushort right)
        //{
        //    return left + new OnesCompliment(right);
        //}

        public static OnesCompliment operator +(OnesCompliment left, OnesCompliment right)
        {
            var sum = left.NativeValue + right.NativeValue;

            // if we have overflow, most likely from subtracting negative numbers
            if ((sum & 0x10000) > 0)
            {
                // we need to ones compliment correct the negative number by adding 1 and taking the lower 16 bits
                // this process is called "end around carry"
                sum = sum + 1;
                sum = sum & 0xFFFF;
            }

            return new OnesCompliment(sum);
        }

        public static bool operator ==(OnesCompliment left, OnesCompliment right)
        {
            return left.NativeValue == right.NativeValue;
        }

        public static bool operator !=(OnesCompliment left, OnesCompliment right)
        {
            return left.NativeValue != right.NativeValue;
        }

        public static bool operator ==(OnesCompliment left, int right)
        {
            return left.NativeValue == (ushort)right;
        }

        public static bool operator !=(OnesCompliment left, int right)
        {
            return left.NativeValue != (ushort)right;
        }

        public override bool Equals(object obj)
        {
            if (obj is OnesCompliment)
            {
                var toCompare = (OnesCompliment)obj;
                return NativeValue == toCompare.NativeValue;
            }
            else if (obj is ushort)
            {
                var toCompare = (ushort)obj;
                return NativeValue == toCompare;
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return NativeValue.GetHashCode();
        }

        /// <summary>
        /// Automatically return ushort value for OnesCompliment value
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        //public static implicit operator ushort(OnesCompliment a)
        //{
        //    return a.RawValue;
        //}

        /// <summary>
        /// Automatically return onescompliment object from ushort value
        /// </summary>
        /// <param name="a"></param>
        //public static implicit operator OnesCompliment(ushort a)
        //{
        //    return new OnesCompliment(a);
        //}

        /// <summary>
        /// The diminished absolute value is defined as DABS(x)=|x|-1 if |x|>1, or +0 otherwise
        /// </summary>
        public OnesCompliment GetDiminishedAbsoluteValue()
        {
            // if negative, NOT 1's to get ABS
            ushort absNative = IsNegative ? (ushort)~NativeValue : NativeValue;

            var abs = new OnesCompliment(absNative);

            if (abs.IsPositive && abs != PositiveZero)
                return abs + NegativeOne;
            else
                return PositiveZero;
        }
    }

    public static class OnesComplimentHelpers
    {
        public static OnesCompliment ToOnesCompliment(this int v)
        {
            return new OnesCompliment(v);
        }

        public static OnesCompliment ToOnesCompliment(this ushort v)
        {
            return new OnesCompliment(v);
        }
    }
}
